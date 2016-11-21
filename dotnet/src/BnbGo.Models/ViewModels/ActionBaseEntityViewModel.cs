using System;
using BnbGo.Models;

namespace BnbGo.Models.ViewModels
{
    public class ActionBaseEntityViewModel<T>: ActionViewModel
    {
        public BaseEntity<T> BaseEntity { get; set; }
    }
}