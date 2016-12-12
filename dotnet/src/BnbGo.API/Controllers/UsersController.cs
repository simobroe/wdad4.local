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
     public class UsersController : BaseController 
     {       
        private const string FAILGETENTITIES = "Failed to get users from the API";
        private const string FAILGETENTITYBYID = "Failed to get user from the API by Id: {0}";


        public UsersController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var model = await ApplicationDbContext.Users.ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{UserId:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var model = await ApplicationDbContext.Users.FirstOrDefaultAsync(o => o.Id == userId);
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, userId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
