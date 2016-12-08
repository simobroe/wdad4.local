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
    public class CurrencyTypeController : BaseController 
    {
        public CurrencyTypeController(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            
        }

        public async Task<IActionResult> Index( string searchString) 
        {
            List<CurrencyType> currencytypes = null;

            if ( !String.IsNullOrEmpty(searchString)) {
                currencytypes = await ApplicationDbContext.CurrencyTypes
                    .Where(o => o.Name.Contains(searchString))
                    .OrderBy(o => o.Name)
                    .ToListAsync();
            } else {
                currencytypes = await ApplicationDbContext.CurrencyTypes
                    .OrderBy(o => o.Name)
                    .ToListAsync();
            }

            if (this.Request.Headers["X-Requested-With"] == "XMLHttpRequest") 
            {
                return PartialView("_ListPartial", currencytypes);
            }
            
            return View(currencytypes);
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id){

            var model = await ApplicationDbContext.CurrencyTypes
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
        public async Task<IActionResult> Create(CurrencyTypeViewModel model)
        {
            var alert = new Alert();
            try
            {
                if(!ModelState.IsValid) {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }
                
                model.CurrencyType.Description = model.CurrencyType.Name;

                ApplicationDbContext.CurrencyTypes.Add(model.CurrencyType);
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

                model = await ViewModel(model.CurrencyType);

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
            
            var model = await ApplicationDbContext.CurrencyTypes.FirstOrDefaultAsync(m => m.Id == id);
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = await ViewModel(model);
            
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CurrencyTypeViewModel model)
        {
            var alert = new Alert();
            try 
            {
                if(!ModelState.IsValid)
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.INVALID;
                    throw new Exception();
                }   
                    
                var originalModel = ApplicationDbContext.CurrencyTypes.FirstOrDefault(m => m.Id == model.CurrencyType.Id);
                
                if(originalModel == null) 
                {
                    alert.Message = alert.ExceptionMessage = ApplicationDbContextMessage.NOTEXISTS;
                    throw new Exception();
                }
                    
                originalModel.Name = model.CurrencyType.Name;
                originalModel.Description = model.CurrencyType.Name;
                
                ApplicationDbContext.CurrencyTypes.Attach(originalModel);
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

                model = await ViewModel(model.CurrencyType);

                ModelState.AddModelError(string.Empty, alert.ExceptionMessage);
            }
            return View(model);
        }

         [HttpGet("[area]/[controller]/[action]/{id:int}")]//SLA
        public async Task<IActionResult> Delete(Int16 id, [FromQuery] ActionType actionType)
        {
            var model = await ApplicationDbContext.CurrencyTypes.FirstOrDefaultAsync(m => m.Id == id);
            
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            
            var viewModel = new ActionCurrencyTypeViewModel()
            {
                BaseEntity = model,
                ActionType = actionType
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ActionCurrencyTypeViewModel model)
        {
            var alert = new Alert();// Alert
            try
            {
                var originalModel = ApplicationDbContext.CurrencyTypes.FirstOrDefault(m => m.Id == model.BaseEntity.Id);
                
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

         private async Task<CurrencyTypeViewModel> ViewModel(CurrencyType currencytype = null) 
        {
            var viewModel = new CurrencyTypeViewModel 
            {
                CurrencyType = (currencytype != null)?currencytype:new CurrencyType()
            };

            return viewModel;
        }
    }
}