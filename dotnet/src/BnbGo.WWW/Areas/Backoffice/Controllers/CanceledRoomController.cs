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
    public class CanceledRoomController : BaseController 
    {
        public CanceledRoomController(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            
        }

        public async Task<IActionResult> Index( string searchString) 
        {
            List<Room> rooms = null;

            if ( !String.IsNullOrEmpty(searchString)) {
                rooms = await ApplicationDbContext.Rooms
                    .Where(o => o.Name.Contains(searchString) && o.RoomStateId == 2 )
                    .OrderBy(o => o.Name)
                    .Include(u => u.User)
                    .ToListAsync();
            } else {
                rooms = await ApplicationDbContext.Rooms
                    .Where(o => o.RoomStateId == 2)
                    .OrderBy(o => o.Name)
                    .Include(u => u.User)
                    .ToListAsync();
            }

            if (this.Request.Headers["X-Requested-With"] == "XMLHttpRequest") 
            {
                return PartialView("_ListPartial", rooms);
            }
            
            return View(rooms);
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id){

            var model = await ApplicationDbContext.Rooms
                .Where(c => c.Id == id)
                .Include(u => u.User)
                .Include(hot => hot.HouseType)
                .Include(rot => rot.RoomType)
                .Include(ret => ret.RentType)
                .Include(loc => loc.Location)
                .Include(cit => cit.City)
                .Include(fac => fac.Facilities)
                .ThenInclude(fac => fac.Facility)
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
        public async Task<IActionResult> Create(RoomViewModel model)
        {
            var alert = new Alert();
            try
            {
                if(!ModelState.IsValid) {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }
                
                ApplicationDbContext.Rooms.Add(model.Room);
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

                model = await ViewModel(model.Room);

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
            
            var model = await ApplicationDbContext.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = await ViewModel(model);
            
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoomViewModel model)
        {
            var alert = new Alert();
            try 
            {
                if(!ModelState.IsValid)
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }   
                    
                var originalModel = ApplicationDbContext.Rooms.FirstOrDefault(m => m.Id == model.Room.Id);
                
                if(originalModel == null) 
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.NOTEXISTS;
                    throw new Exception();
                }
                    
                originalModel.Name = model.Room.Name;
                originalModel.Description = model.Room.Description;
                originalModel.UserId = model.Room.UserId;
                originalModel.HouseTypeId = model.Room.HouseTypeId;
                originalModel.RoomTypeId = model.Room.RoomTypeId;
                originalModel.RentTypeId = model.Room.RentTypeId;
                originalModel.RoomStateId = model.Room.RoomStateId;
                originalModel.LocationId = model.Room.LocationId;
                originalModel.CityId = model.Room.CityId;
                
                ApplicationDbContext.Rooms.Attach(originalModel);
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

                model = await ViewModel(model.Room);

                ModelState.AddModelError(string.Empty, alert.ExceptionMessage);
            }
            return View(model);
        }

         [HttpGet("[area]/[controller]/[action]/{id:int}")]//SLA
        public async Task<IActionResult> Delete(Int16 id, [FromQuery] ActionType actionType)
        {
            var model = await ApplicationDbContext.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            
            var viewModel = new ActionRoomViewModel()
            {
                BaseEntity = model,
                ActionType = actionType
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ActionRoomViewModel model)
        {
            var alert = new Alert();// Alert
            try
            {
                var originalModel = ApplicationDbContext.Rooms.FirstOrDefault(m => m.Id == model.BaseEntity.Id);
                
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

         private async Task<RoomViewModel> ViewModel(Room room = null) 
        {
            var users = await ApplicationDbContext.Users.OrderBy(u => u.FirstName).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.FirstName + " " + o.SurName
            }).ToListAsync();
            var roomtypes = await ApplicationDbContext.RoomTypes.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name
            }).ToListAsync();
            var renttypes = await ApplicationDbContext.RentTypes.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name
            }).ToListAsync();
            var housetypes = await ApplicationDbContext.HouseTypes.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name
            }).ToListAsync();
            var roomstates = await ApplicationDbContext.RoomStates.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name
            }).ToListAsync();
            var locations = await ApplicationDbContext.Locations.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name + " " + o.Description
            }).ToListAsync();
            var cities = await ApplicationDbContext.Cities.OrderBy(u => u.Name).Select(o => new SelectListItem { 
                Value = o.Id.ToString(), 
                Text = o.Name
            }).ToListAsync();

            var viewModel = new RoomViewModel 
            {
                Room = (room != null)?room:new Room(),
                Users = users,
                HouseTypes = housetypes,
                RoomTypes = roomtypes,
                RentTypes = renttypes,
                RoomStates = roomstates,
                Locations = locations,
                Cities = cities
            };

            return viewModel;
        }
    }
}