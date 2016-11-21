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
     public class ReservationsController : BaseController 
     {       
        private const string FAILGETENTITIES = "Failed to get Reservations from the API";
        private const string FAILGETENTITYBYID = "Failed to get reservation from the API by Id: {0}";


        public ReservationsController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        [HttpGet(Name = "GetReservations")]
        public async Task<IActionResult> GetReservations()
        {
            var model = await ApplicationDbContext.Reservations.ToListAsync();
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{reservationId:int}", Name = "GetReservationById")]
        public async Task<IActionResult> GetReservationById(Int16 reservationId)
        {
            var model = await ApplicationDbContext.Reservations.FirstOrDefaultAsync(o => o.Id == reservationId);
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, reservationId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
