using System;
using BnbGo.Models;
using BnbGo.Models.Utilities;

namespace BnbGo.Models.ViewModels
{
    public abstract class ActionViewModel
    {
        public ActionType ActionType { get; set; }
        public string Action 
        {
            get
            {
                switch(ActionType)
                {
                    case ActionType.Delete: return "delete";
                    case ActionType.SoftDelete: return "soft-delete";
                    case ActionType.SoftUnDelete: return "soft-undelete";
                    default: return "nothing";
                }
            }
        }
    }
}