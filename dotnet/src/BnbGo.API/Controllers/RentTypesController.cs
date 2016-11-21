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
     public class RentTypesController : BaseController 
     {       
        private const string FAILGETENTITIES = "Failed to get renttypes from the API";
        private const string FAILGETENTITYBYID = "Failed to get renttype from the API by Id: {0}";


        public RentTypesController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        [HttpGet(Name = "GetRentTypes")]
        public async Task<IActionResult> GetRentTypes()
        {
            var model = await ApplicationDbContext.RentTypes.ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{renttypeId:int}", Name = "GetRentTypeById")]
        public async Task<IActionResult> GetRentTypeById(Int16 renttypeId)
        {
            var model = await ApplicationDbContext.RentTypes.FirstOrDefaultAsync(o => o.Id == renttypeId);
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, renttypeId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
