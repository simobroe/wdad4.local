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
     public class CitiesController : BaseController 
     {       
        private const string FAILGETENTITIES = "Failed to get cities from the API";
        private const string FAILGETENTITYBYID = "Failed to get city from the API by Id: {0}";


        public CitiesController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        [HttpGet(Name = "GetCities")]
        public async Task<IActionResult> GetCities()
        {
            var model = await ApplicationDbContext.Cities.ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{regionId:int}", Name = "GetCityByRegion")]
        public async Task<IActionResult> GetCityByRegion(Int16 regionId)
        {
            var model = await ApplicationDbContext.Cities.Where(o => o.RegionId == regionId).ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, regionId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
