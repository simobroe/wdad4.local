using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BnbGo.Models;

namespace BnbGo.Models.ViewModels
{
    public class RegionViewModel
    {
        public Region Region { get; set; }
        public List<SelectListItem> Country { get; set; }
    }
}