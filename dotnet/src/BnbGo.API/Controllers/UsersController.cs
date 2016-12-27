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

        [HttpGet("byId/{UserId:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var model = await ApplicationDbContext.Users
                                    .Where(o => o.Id == userId)
                                    .Include(reg => reg.Region)
                                    .Include(cit => cit.City)
                                    .Include(cou => cou.Country)
                                    .Include(ro => ro.Rooms)
                                        .ThenInclude(ro => ro.RoomState)
                                    .Include(ro => ro.Rooms)
                                        .ThenInclude(ro => ro.HouseType)
                                    .Include(ro => ro.Rooms)
                                        .ThenInclude(ro => ro.RoomType)
                                    .Include(ro => ro.Rooms)
                                        .ThenInclude(ro => ro.RentType)
                                    .Include(ro => ro.Rooms)
                                        .ThenInclude(ro => ro.City)
                                    .Include(ro => ro.Rooms)
                                        .ThenInclude(ro => ro.City.Region)
                                    .Include(ro => ro.Rooms)
                                        .ThenInclude(ro => ro.Images)
                                    .Include(re => re.Reservations)
                                        .ThenInclude(res => res.Room)
                                    .Include(re => re.Reservations)
                                        .ThenInclude(res => res.Room.City.Region.Country.CurrencyType)
                                    .Include(im => im.Images)
                                    .Include(re => re.Reservations)
                                    .ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, userId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("byEmail/{UserEmail}", Name = "GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string userEmail)
        {
            var model = await ApplicationDbContext.Users.FirstOrDefaultAsync(o => o.Email == userEmail);
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, userEmail);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser user) {
            if (user == null) {
                return BadRequest();
            }
            ApplicationDbContext.Users.Add(user);
            await ApplicationDbContext.SaveChangesAsync();

            return Json("user data recieved");
        }
    }
}
