using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BnbGo.Models.Security;
using BnbGo.Models;

namespace BnbGo.Models.ViewModels
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }
        public List<SelectListItem> Regions { get; set; }
    }
}