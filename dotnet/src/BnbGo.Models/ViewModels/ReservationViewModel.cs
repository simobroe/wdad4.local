using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BnbGo.Models;

namespace BnbGo.Models.ViewModels
{
    public class ReservationViewModel
    {
        public Reservation Reservation { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> Rooms { get; set; }
    }
}