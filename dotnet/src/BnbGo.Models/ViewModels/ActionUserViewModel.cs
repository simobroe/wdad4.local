using System;
using BnbGo.Models;
using BnbGo.Models.Security;

namespace BnbGo.Models.ViewModels
{
    public class ActionUserViewModel : ActionViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
    }
}