using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BnbGo.Db;
using BnbGo.Models;
using BnbGo.Models.ViewModels;
using BnbGo.Models.Security;

namespace  BnbGo.WWW.Areas.Backoffice.ViewComponents 
{
    [ViewComponent(Name="AmountForEntity")]
    public class AmountForEntityViewComponent : BaseViewComponent 
    {
        public AmountForEntityViewComponent(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(string entityType)
        {
            var viewModel = await GetAmountForEntityAsync(entityType);
            return View(viewModel);
        }

        private Task<AmountForEntityViewModel> GetAmountForEntityAsync(string entityType)
        {
            return Task.FromResult(GetAmountForEntity(entityType));
        }

        private AmountForEntityViewModel GetAmountForEntity(string entityType)
        {
            var amount = 0;
            entityType = entityType;
            var name = entityType;
            var pluralizeName = "Entities";

            switch(entityType)
            {
                case "Country":
                    amount = ApplicationDbContext.Countries.AsEnumerable().Count();
                    entityType = "Country";
                    name = "Country";
                    pluralizeName = "Countries";
                    break;
                case "Region":
                    amount = ApplicationDbContext.Regions.AsEnumerable().Count();
                    entityType = "Region";
                    name = "Region";
                    pluralizeName = "Regions";
                    break;
                case "City":
                    amount = ApplicationDbContext.Cities.AsEnumerable().Count();
                    entityType = "City";
                    name = "City";
                    pluralizeName = "Cities";
                    break;
                case "Room":
                    amount = ApplicationDbContext.Rooms.AsEnumerable().Count();
                    entityType = "Room";
                    name = "Room";
                    pluralizeName = "Rooms";
                    break;
                case "Reservation":
                    amount = ApplicationDbContext.Reservations.AsEnumerable().Count();
                    entityType = "Reservation";
                    name = "Reservation";
                    pluralizeName = "Reservations";
                    break;
                case "CurrencyType":
                    amount = ApplicationDbContext.CurrencyTypes.AsEnumerable().Count();
                    entityType = "CurrencyType";
                    name = "currency type";
                    pluralizeName = "Currency types";
                    break;
                case "Facility":
                    amount = ApplicationDbContext.Facilities.AsEnumerable().Count();
                    entityType = "Facility";
                    name = "Facility";
                    pluralizeName = "Facilities";
                    break;
                case "HouseType":
                    amount = ApplicationDbContext.HouseTypes.AsEnumerable().Count();
                    entityType = "HouseType";
                    name = "House type";
                    pluralizeName = "House types";
                    break;
                case "RoomType":
                    amount = ApplicationDbContext.RoomTypes.AsEnumerable().Count();
                    entityType = "RoomType";
                    name = "Room type";
                    pluralizeName = "Room types";
                    break;
                case "RentType":
                    amount = ApplicationDbContext.RentTypes.AsEnumerable().Count();
                    entityType = "RentType";
                    name = "Rent type";
                    pluralizeName = "Rent types";
                    break;
                case "User":
                    amount = ApplicationDbContext.Users.AsEnumerable().Count();
                    entityType = "User";
                    name = "User";
                    pluralizeName = "Users";
                    break;
            }

            var viewModel = new AmountForEntityViewModel()
            {
                Amount = amount,
                EntityType = entityType,
                Name = name,
                PluralizeName = pluralizeName
            };

            return viewModel;
        }
    }
}