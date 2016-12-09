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
    public class ImageController : BaseController 
    {
        public ImageController(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            
        }

        public async Task<IActionResult> Index( string searchString) 
        {
            List<Image> images = null;

            if ( !String.IsNullOrEmpty(searchString)) {
                images = await ApplicationDbContext.Images
                    .Where(o => o.Name.Contains(searchString))
                    .OrderBy(o => o.Name)
                    .Include(it => it.ImageType)
                    .ToListAsync();
            } else {
                images = await ApplicationDbContext.Images
                    .OrderBy(o => o.Name)
                    .Include(it => it.ImageType)
                    .ToListAsync();
            }

            if (this.Request.Headers["X-Requested-With"] == "XMLHttpRequest") 
            {
                return PartialView("_ListPartial", images);
            }
            
            return View(images);
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id){

            var model = await ApplicationDbContext.Images
                .Where(c => c.Id == id)
                .Include(it => it.ImageType)
                .Include(us => us.User)
                .Include(re => re.Region)
                .Include(ro => ro.Room)
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
        public async Task<IActionResult> Create(ImageViewModel model)
        {
            var alert = new Alert();
            try
            {
                if(!ModelState.IsValid) {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }
                
                model.Image.Description = model.Image.Name;
                if (model.Image.ImageTypeId == 1) {
                    model.Image.RegionId = null;
                    model.Image.RoomId = null;
                } else if (model.Image.ImageTypeId == 2) {
                    model.Image.UserId = null;
                    model.Image.RegionId = null;
                } else {
                    model.Image.UserId = null;
                    model.Image.RoomId = null;
                }

                ApplicationDbContext.Images.Add(model.Image);
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

                model = await ViewModel(model.Image);

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
            
            var model = await ApplicationDbContext.Images.FirstOrDefaultAsync(m => m.Id == id);
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = await ViewModel(model);
            
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ImageViewModel model)
        {
            var alert = new Alert();
            try 
            {
                if(!ModelState.IsValid)
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }   
                    
                var originalModel = ApplicationDbContext.Images.FirstOrDefault(m => m.Id == model.Image.Id);
                
                if(originalModel == null) 
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.NOTEXISTS;
                    throw new Exception();
                }
                    
                originalModel.Name = model.Image.Name;
                originalModel.Description = model.Image.Name;
                originalModel.Link = model.Image.Link;
                originalModel.ImageTypeId = model.Image.ImageTypeId;
                if (model.Image.ImageTypeId == 1) {
                    originalModel.UserId = model.Image.UserId;
                    originalModel.RegionId = null;
                    originalModel.RoomId = null;
                } else if (model.Image.ImageTypeId == 2) {
                    originalModel.UserId = null;
                    originalModel.RegionId = null;
                    originalModel.RoomId = model.Image.RoomId;
                } else {
                    originalModel.UserId = null;
                    originalModel.RegionId = model.Image.RegionId;
                    originalModel.RoomId = null;
                }
                
                ApplicationDbContext.Images.Attach(originalModel);
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

                model = await ViewModel(model.Image);

                ModelState.AddModelError(string.Empty, alert.ExceptionMessage);
            }
            return View(model);
        }

         [HttpGet("[area]/[controller]/[action]/{id:int}")]//SLA
        public async Task<IActionResult> Delete(Int16 id, [FromQuery] ActionType actionType)
        {
            var model = await ApplicationDbContext.Images.FirstOrDefaultAsync(m => m.Id == id);
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            
            var viewModel = new ActionImageViewModel()
            {
                BaseEntity = model,
                ActionType = actionType
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ActionImageViewModel model)
        {
            var alert = new Alert();// Alert
            try
            {
                var originalModel = ApplicationDbContext.Images.FirstOrDefault(m => m.Id == model.BaseEntity.Id);
                
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

         private async Task<ImageViewModel> ViewModel(Image image = null) 
        {
            var imagetypes = await ApplicationDbContext.ImageTypes.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name 
            }).ToListAsync();
            var users = await ApplicationDbContext.Users.OrderBy(u => u.FirstName).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.FirstName + " " + o.SurName
            }).ToListAsync();
            var regions = await ApplicationDbContext.Regions.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name 
            }).ToListAsync();
            var rooms = await ApplicationDbContext.Rooms.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name 
            }).ToListAsync();

            var viewModel = new ImageViewModel 
            {
                Image = (image != null)?image:new Image(),
                ImageTypes = imagetypes,
                Users = users,
                Regions = regions,
                Rooms = rooms
            };

            return viewModel;
        }
    }
}