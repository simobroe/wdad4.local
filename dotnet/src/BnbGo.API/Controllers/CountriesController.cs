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
     public class CountriesController : BaseController 
     {       
        private const string FAILGETENTITIES = "Failed to get countries from the API";
        private const string FAILGETENTITYBYID = "Failed to get country from the API by Id: {0}";


        public CountriesController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        [HttpGet(Name = "GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var model = await ApplicationDbContext.Countries.ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{countryId:int}", Name = "GetCountryById")]
        public async Task<IActionResult> GetCountryById(Int16 countryId)
        {
            var model = await ApplicationDbContext.Countries.FirstOrDefaultAsync(o => o.Id == countryId);
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, countryId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
