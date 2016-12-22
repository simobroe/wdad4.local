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
     public class RoomsController : BaseController 
     {       
        private const string FAILGETENTITIES = "Failed to get rooms from the API";
        private const string FAILGETENTITYBYID = "Failed to get room from the API by Id: {0}";
        private const string FAILGETENTITYBYCITYID = "Failed to get room from the API by CityId: {0}";


        public RoomsController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        [HttpGet(Name = "GetRooms")]
        public async Task<IActionResult> GetRooms()
        {
            var model = await ApplicationDbContext.Rooms.ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("ById/{Id:int}", Name = "GetRoomById")]
        public async Task<IActionResult> GetRoomById(Int16 Id)
        {
            var model = await ApplicationDbContext.Rooms
                .Where(o => o.Id == Id)
                .OrderBy(o => o.Name)
                .Include(loc => loc.Location)
                .Include(rot => rot.RoomType)
                .Include(rt => rt.Ratings)
                .Include(im => im.Images)
                .Include(u => u.User)
                .Include(um => um.User.Images)
                .Include(ret => ret.RentType)
                .Include(ht => ht.HouseType)
                .Include(rg => rg.City.Region)
                .ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, Id);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("ByCityId/{cityId:int}", Name = "GetRoomByCityId")]
        public async Task<IActionResult> GetRoomByCityId(Int16 cityId)
        {
            var model = await ApplicationDbContext.Rooms
                .Where(o => o.CityId == cityId)
                .Include(loc => loc.Location)
                .Include(rot => rot.RoomType)
                .Include(rt => rt.Ratings)
                .Include(im => im.Images)
                .Include(u => u.User)
                .Include(um => um.User.Images)
                .Include(ret => ret.RentType)
                .Include(ht => ht.HouseType)
                .Include(rg => rg.City.Region)
                .ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYCITYID, cityId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
