using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BnbGo.Db;
using BnbGo.Models.Security;

namespace BnbGo.API.Controllers {
    public class BaseController : Controller {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        public BaseController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager) {
            ApplicationDbContext = applicationDbContext;
            UserManager = userManager;
        }
    }
}