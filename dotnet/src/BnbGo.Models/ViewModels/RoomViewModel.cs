using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BnbGo.Models;

namespace BnbGo.Models.ViewModels
{
    public class RoomViewModel
    {
        public Room Room { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> HouseTypes { get; set; }
        public List<SelectListItem> RoomTypes { get; set; }
        public List<SelectListItem> RentTypes { get; set; }
    }
}