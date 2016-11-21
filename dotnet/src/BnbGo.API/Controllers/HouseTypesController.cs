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
     public class HouseTypesController : BaseController 
     {       
        private const string FAILGETENTITIES = "Failed to get housetypes from the API";
        private const string FAILGETENTITYBYID = "Failed to get housetype from the API by Id: {0}";


        public HouseTypesController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        [HttpGet(Name = "GetHouseTypes")]
        public async Task<IActionResult> GetHouseTypes()
        {
            var model = await ApplicationDbContext.HouseTypes.ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{housetypeId:int}", Name = "GetHouseTypeById")]
        public async Task<IActionResult> GetHouseTypeById(Int16 housetypeId)
        {
            var model = await ApplicationDbContext.HouseTypes.FirstOrDefaultAsync(o => o.Id == housetypeId);
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, housetypeId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
