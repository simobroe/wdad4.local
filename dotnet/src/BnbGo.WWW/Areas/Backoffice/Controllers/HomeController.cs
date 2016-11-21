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
    public class HomeController : BaseController 
    {
        public HomeController():base()
        {
        }

        public IActionResult Index() 
        {
            return View();
        }
    }
}