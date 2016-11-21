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
     public class RegionsController : BaseController 
     {       
        private const string FAILGETENTITIES = "Failed to get regions from the API";
        private const string FAILGETENTITYBYID = "Failed to get region from the API by Id: {0}";


        public RegionsController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        [HttpGet(Name = "GetRegions")]
        public async Task<IActionResult> GetRegions()
        {
            var model = await ApplicationDbContext.Regions.ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{regionId:int}", Name = "GetRegionById")]
        public async Task<IActionResult> GetRegionById(Int16 regionId)
        {
            var model = await ApplicationDbContext.Regions.FirstOrDefaultAsync(o => o.Id == regionId);
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, regionId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}