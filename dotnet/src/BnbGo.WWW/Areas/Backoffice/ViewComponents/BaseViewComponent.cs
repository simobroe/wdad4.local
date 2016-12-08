using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BnbGo.Db;
using BnbGo.Models;
using BnbGo.Models.ViewModels;
using BnbGo.Models.Security;

namespace  BnbGo.WWW.Areas.Backoffice.ViewComponents 
{
    public abstract class BaseViewComponent : ViewComponent 
    {
        public ApplicationDbContext ApplicationDbContext { get; set; }
        public UserManager<ApplicationUser>  ApplicationUserManager  { get; set; }
        public RoleManager<ApplicationRole>  ApplicationRoleManager  { get; set; }
        
        public BaseViewComponent() 
        {
        }

        public BaseViewComponent([FromServices]ApplicationDbContext applicationDbContext) 
        {
            ApplicationDbContext = applicationDbContext;
        }

        public BaseViewComponent([FromServices]ApplicationDbContext applicationDbContext, [FromServices]UserManager<ApplicationUser>  ApplicationUserManager, [FromServices]RoleManager<ApplicationRole>  ApplicationRoleManager) 
        {
            ApplicationDbContext = applicationDbContext;
        }
    }
}