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
    public class RoomTypeController : BaseController 
    {
        public RoomTypeController(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            
        }

        public async Task<IActionResult> Index( string searchString) 
        {
            List<RoomType> roomtypes = null;

            if ( !String.IsNullOrEmpty(searchString)) {
                roomtypes = await ApplicationDbContext.RoomTypes
                    .Where(o => o.Name.Contains(searchString))
                    .OrderBy(o => o.Name)
                    .ToListAsync();
            } else {
                roomtypes = await ApplicationDbContext.RoomTypes
                    .OrderBy(o => o.Name)
                    .ToListAsync();
            }

            if (this.Request.Headers["X-Requested-With"] == "XMLHttpRequest") 
            {
                return PartialView("_ListPartial", roomtypes);
            }
            
            return View(roomtypes);
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id){

            var model = await ApplicationDbContext.RoomTypes
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
        public async Task<IActionResult> Create(RoomTypeViewModel model)
        {
            var alert = new Alert();
            try
            {
                if(!ModelState.IsValid) {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }
                
                ApplicationDbContext.RoomTypes.Add(model.RoomType);
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

                model = await ViewModel(model.RoomType);

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
            
            var model = await ApplicationDbContext.RoomTypes.FirstOrDefaultAsync(m => m.Id == id);
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = await ViewModel(model);
            
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoomTypeViewModel model)
        {
            var alert = new Alert();
            try 
            {
                if(!ModelState.IsValid)
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }   
                    
                var originalModel = ApplicationDbContext.RoomTypes.FirstOrDefault(m => m.Id == model.RoomType.Id);
                
                if(originalModel == null) 
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.NOTEXISTS;
                    throw new Exception();
                }
                    
                originalModel.Name = model.RoomType.Name;
                originalModel.Description = model.RoomType.Description;
                
                ApplicationDbContext.RoomTypes.Attach(originalModel);
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

                model = await ViewModel(model.RoomType);

                ModelState.AddModelError(string.Empty, alert.ExceptionMessage);
            }
            return View(model);
        }

         [HttpGet("[area]/[controller]/[action]/{id:int}")]//SLA
        public async Task<IActionResult> Delete(Int16 id, [FromQuery] ActionType actionType)
        {
            var model = await ApplicationDbContext.RoomTypes.FirstOrDefaultAsync(m => m.Id == id);
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            
            var viewModel = new ActionRoomTypeViewModel()
            {
                BaseEntity = model,
                ActionType = actionType
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ActionRoomTypeViewModel model)
        {
            var alert = new Alert();// Alert
            try
            {
                var originalModel = ApplicationDbContext.RoomTypes.FirstOrDefault(m => m.Id == model.BaseEntity.Id);
                
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

         private async Task<RoomTypeViewModel> ViewModel(RoomType roomtype = null) 
        {
            var viewModel = new RoomTypeViewModel 
            {
                RoomType = (roomtype != null)?roomtype:new RoomType()
            };

            return viewModel;
        }
    }
}