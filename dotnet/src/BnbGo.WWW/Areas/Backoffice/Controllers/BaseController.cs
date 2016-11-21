using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BnbGo.Db;
using BnbGo.Models;
using BnbGo.Models.Security;

namespace  BnbGo.WWW.Areas.Backoffice.Controllers 
{
    [Area("Backoffice")]
    public abstract class BaseController : Controller 
    {
        public ApplicationDbContext ApplicationDbContext { get; set; }
        public UserManager<ApplicationUser>  ApplicationUserManager  { get; set; }
        public RoleManager<ApplicationRole>  ApplicationRoleManager  { get; set; }
        
        public BaseController() 
        {
        }

        public BaseController([FromServices]ApplicationDbContext applicationDbContext) 
        {
            ApplicationDbContext = applicationDbContext;
        }

        public BaseController([FromServices]ApplicationDbContext applicationDbContext, [FromServices]UserManager<ApplicationUser>  ApplicationUserManager, [FromServices]RoleManager<ApplicationRole>  ApplicationRoleManager) 
        {
            ApplicationDbContext = applicationDbContext;
        }
    }
}