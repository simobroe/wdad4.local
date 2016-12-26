using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BnbGo.Db;
using BnbGo.Models;
using BnbGo.Models.Security;
using BnbGo.Models.Utilities;
using BnbGo.Models.ViewModels;

namespace BnbGo.WWW.Areas.Backoffice.Controllers 
{
    [Area("Backoffice")]
    public class UserController : BaseController 
    {
        public UserController(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            
        }

        public async Task<IActionResult> Index( string searchString) 
        {
            List<ApplicationUser> users = null;

            if ( !String.IsNullOrEmpty(searchString)) {
                users = await ApplicationDbContext.Users
                    .Where(o => o.SurName.Contains(searchString) || o.FirstName.Contains(searchString))
                    .OrderBy(o => o.FirstName)
                    .Include(co => co.Country)
                    .Include(re => re.Region)
                    .Include(ci => ci.City)
                    .ToListAsync();
            } else {
                users = await ApplicationDbContext.Users
                    .OrderBy(o => o.FirstName)
                    .Include(co => co.Country)
                    .Include(re => re.Region)
                    .Include(ci => ci.City)
                    .ToListAsync();
            }

            if (this.Request.Headers["X-Requested-With"] == "XMLHttpRequest") 
            {
                return PartialView("_ListPartial", users);
            }
            
            return View(users);
        }
        

        [HttpGet]
        public async Task<IActionResult> Show(string id){

            var model = await ApplicationDbContext.Users
                .Where(c => c.Id == new Guid(id))
                    .Include(co => co.Country)
                    .Include(re => re.Region)
                    .Include(ci => ci.City)
                .ToListAsync();

            if(this.Request.Headers["X-Requested-With"] == "XMLHttpRequest"){
                return PartialView("_DetailPartial", model);
            }
            
            return View(model);

            }

         public async Task<IActionResult> Create() 
        {  
            var viewModel = await ViewModel();
            
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            var alert = new Alert();
            try
            {
                if(!ModelState.IsValid) {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }
                model.User.UserName = model.User.FirstName + model.User.SurName;
                ApplicationDbContext.Users.Add(model.User);
                if (await ApplicationDbContext.SaveChangesAsync() == 0)
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.CREATENOK;
                    throw new Exception();
                }   

                alert.Message = ApplicationDbContextMessage.CREATEOK;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                alert.Type = AlertType.Error;
                alert.ExceptionMessage = ex.Message;

                model = await ViewModel(model.User);

                ModelState.AddModelError(string.Empty, alert.ExceptionMessage);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            
            var model = await ApplicationDbContext.Users.FirstOrDefaultAsync(m => m.Id == new Guid(id));
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = await ViewModel(model);
            
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var alert = new Alert();
            try 
            {
                if(!ModelState.IsValid)
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }   
                    
                var originalModel = ApplicationDbContext.Users.FirstOrDefault(m => m.Id == model.User.Id);
                
                if(originalModel == null) 
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.NOTEXISTS;
                    throw new Exception();
                }
                    
                originalModel.FirstName = model.User.FirstName;
                originalModel.SurName = model.User.SurName;
                originalModel.Email = model.User.Email;
                originalModel.PasswordHash =  model.User.PlainPassword;
                originalModel.UserName = model.User.UserName;
                originalModel.DayOfBirth = model.User.DayOfBirth;
                
                ApplicationDbContext.Users.Attach(originalModel);
                ApplicationDbContext.Entry(originalModel).State = EntityState.Modified;
                
                if (await ApplicationDbContext.SaveChangesAsync() == 0)
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.EDITNOK;
                    throw new Exception();
                } 
                
                alert.Message = ApplicationDbContextMessage.EDITOK;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                alert.Type = AlertType.Error;
                alert.ExceptionMessage = ex.Message;

                model = await ViewModel(model.User);

                ModelState.AddModelError(string.Empty, alert.ExceptionMessage);
            }
            return View(model);
        }

         [HttpGet("[area]/[controller]/[action]/{id:guid}")]//SLA
        public async Task<IActionResult> Delete(string id, [FromQuery] ActionType actionType)
        {
            var model = await ApplicationDbContext.Users.FirstOrDefaultAsync(m => m.Id == new Guid(id));
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            
            var viewModel = new ActionUserViewModel()
            {
                ApplicationUser = model,
                ActionType = actionType
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ActionUserViewModel model)
        {
            var alert = new Alert();// Alert
            try
            {
                var originalModel = ApplicationDbContext.Users.FirstOrDefault(m => m.Id == model.ApplicationUser.Id);
                
                if(originalModel == null)
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.NOTEXISTS;
                    throw new Exception();
                }

                switch(model.ActionType)
                {
                    case ActionType.Delete:
                        alert.Message = ApplicationDbContextMessage.DELETEOK;
                        ApplicationDbContext.Entry(originalModel).State = EntityState.Deleted;
                        break;
                    case ActionType.SoftDelete:
                        alert.Message = ApplicationDbContextMessage.SOFTDELETEOK;
                        originalModel.DeletedAt = DateTime.Now;
                        ApplicationDbContext.Entry(originalModel).State = EntityState.Modified;
                        break;
                    case ActionType.SoftUnDelete:
                        alert.Message = ApplicationDbContextMessage.SOFTUNDELETEOK;
                        originalModel.DeletedAt = (Nullable<DateTime>)null;
                        ApplicationDbContext.Entry(originalModel).State = EntityState.Modified;
                        break;
                }

                
                if (await ApplicationDbContext.SaveChangesAsync() == 0)
                {                   
                    switch(model.ActionType)
                    {
                        case ActionType.Delete:
                            alert.Message = ApplicationDbContextMessage.DELETENOK;
                            break;
                        case ActionType.SoftDelete:
                            alert.Message = ApplicationDbContextMessage.SOFTDELETENOK;
                            break;
                        case ActionType.SoftUnDelete:
                            alert.Message = ApplicationDbContextMessage.SOFTUNDELETENOK;
                            break;
                    }
                    throw new Exception();
                } 

                if (this.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(alert);
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                alert.Type = AlertType.Error;
                alert.ExceptionMessage = ex.Message;

                if (this.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(alert);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }  

        
         private async Task<UserViewModel> ViewModel(ApplicationUser user = null) 
        {
            var countries = await ApplicationDbContext.Countries.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name 
            }).ToListAsync();
            var regions = await ApplicationDbContext.Regions.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name 
            }).ToListAsync();
            var cities = await ApplicationDbContext.Cities.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name 
            }).ToListAsync();

            var viewModel = new UserViewModel 
            {
                User = (user != null)?user:new ApplicationUser(),
                Countries = countries,
                Regions = regions,
                Cities = cities
            };

            return viewModel;
        }
    }
}