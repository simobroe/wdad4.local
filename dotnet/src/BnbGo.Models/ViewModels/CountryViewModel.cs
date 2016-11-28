using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BnbGo.Models;

namespace BnbGo.Models.ViewModels
{
    public class CountryViewModel
    {
        public Country Country { get; set; }
        public List<SelectListItem> CurrencyTypes { get; set; }
    }
}