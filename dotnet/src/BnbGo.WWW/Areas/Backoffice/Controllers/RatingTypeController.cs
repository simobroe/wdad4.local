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
    public class RatingTypeController : BaseController 
    {
        public RatingTypeController(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            
        }

        public async Task<IActionResult> Index( string searchString) 
        {
            List<RatingType> ratingtypes = null;

            if ( !String.IsNullOrEmpty(searchString)) {
                ratingtypes = await ApplicationDbContext.RatingTypes
                    .Where(o => o.Name.Contains(searchString))
                    .OrderBy(o => o.Name)
                    .ToListAsync();
            } else {
                ratingtypes = await ApplicationDbContext.RatingTypes
                    .OrderBy(o => o.Name)
                    .ToListAsync();
            }

            if (this.Request.Headers["X-Requested-With"] == "XMLHttpRequest") 
            {
                return PartialView("_ListPartial", ratingtypes);
            }
            
            return View(ratingtypes);
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id){

            var model = await ApplicationDbContext.RatingTypes
                .Where(c => c.Id == id)
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
        public async Task<IActionResult> Create(RatingTypeViewModel model)
        {
            var alert = new Alert();
            try
            {
                if(!ModelState.IsValid) {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }
                
                ApplicationDbContext.RatingTypes.Add(model.RatingType);
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

                model = await ViewModel(model.RatingType);

                ModelState.AddModelError(string.Empty, alert.ExceptionMessage);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            
            var model = await ApplicationDbContext.RatingTypes.FirstOrDefaultAsync(m => m.Id == id);
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = await ViewModel(model);
            
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RatingTypeViewModel model)
        {
            var alert = new Alert();
            try 
            {
                if(!ModelState.IsValid)
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }   
                    
                var originalModel = ApplicationDbContext.RatingTypes.FirstOrDefault(m => m.Id == model.RatingType.Id);
                
                if(originalModel == null) 
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.NOTEXISTS;
                    throw new Exception();
                }
                    
                originalModel.Name = model.RatingType.Name;
                originalModel.Description = model.RatingType.Description;
                
                ApplicationDbContext.RatingTypes.Attach(originalModel);
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

                model = await ViewModel(model.RatingType);

                ModelState.AddModelError(string.Empty, alert.ExceptionMessage);
            }
            return View(model);
        }

         [HttpGet("[area]/[controller]/[action]/{id:int}")]//SLA
        public async Task<IActionResult> Delete(Int16 id, [FromQuery] ActionType actionType)
        {
            var model = await ApplicationDbContext.RatingTypes.FirstOrDefaultAsync(m => m.Id == id);
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            
            var viewModel = new ActionRatingTypeViewModel()
            {
                BaseEntity = model,
                ActionType = actionType
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ActionRatingTypeViewModel model)
        {
            var alert = new Alert();// Alert
            try
            {
                var originalModel = ApplicationDbContext.RatingTypes.FirstOrDefault(m => m.Id == model.BaseEntity.Id);
                
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

         private async Task<RatingTypeViewModel> ViewModel(RatingType ratingtype = null) 
        {
            var viewModel = new RatingTypeViewModel 
            {
                RatingType = (ratingtype != null)?ratingtype:new RatingType()
            };

            return viewModel;
        }
    }
}