using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BnbGo.Models;

namespace BnbGo.Models.ViewModels
{
    public class ImageViewModel
    {
        public Image Image { get; set; }
        public List<SelectListItem> ImageTypes { get; set; }
        public List<SelectListItem> Regions { get; set; }
        public List<SelectListItem> Rooms { get; set; }
        public List<SelectListItem> Users { get; set; }
    }
}