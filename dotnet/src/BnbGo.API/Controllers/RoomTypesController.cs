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

namespace BnbGo.API.Controllers
{
    [Route("api/[controller]")]
     public class RoomTypesController : BaseController 
     {       
        private const string FAILGETENTITIES = "Failed to get roomtypes from the API";
        private const string FAILGETENTITYBYID = "Failed to get roomtype from the API by Id: {0}";


        public RoomTypesController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        [HttpGet(Name = "GetRoomTypes")]
        public async Task<IActionResult> GetRoomTypes()
        {
            var model = await ApplicationDbContext.RoomTypes.ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{roomtypeId:int}", Name = "GetRoomTypeById")]
        public async Task<IActionResult> GetRoomTypeById(Int16 roomtypeId)
        {
            var model = await ApplicationDbContext.RoomTypes.FirstOrDefaultAsync(o => o.Id == roomtypeId);
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, roomtypeId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
