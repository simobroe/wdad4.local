using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BnbGo.Models;

namespace BnbGo.Models.ViewModels
{
    public class CityViewModel
    {
        public City City { get; set; }
        public List<SelectListItem> Regions { get; set; }
    }
}