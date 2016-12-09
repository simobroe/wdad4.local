using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BnbGo.Models;

namespace BnbGo.Models.ViewModels
{
    public class LocationViewModel
    {
        public Location Location { get; set; }
        public List<SelectListItem> Cities { get; set; }
    }
}