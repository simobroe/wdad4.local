using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BnbGo.Db;
using BnbGo.Models;
using BnbGo.Models.Security;
using BnbGo.Models.Utilities;

namespace BnbGo.WWW.Areas.Backoffice.Controllers 
{
    [Area("Backoffice")]
    public class RoomTypeController : BaseController 
    {
        public RoomTypeController(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
        }

        public async Task<IActionResult> Index() 
        {
            var model = await ApplicationDbContext.RoomTypes.OrderBy(o => o.Name).ToListAsync();

            if (this.Request.Headers["X-Requested-With"] == "XMLHttpRequest") 
            {
                return PartialView("_ListPartial", model);
            }
            
            return View(model);

        }
    }
}