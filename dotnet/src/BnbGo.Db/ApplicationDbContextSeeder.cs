using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions;
using BnbGo.Models;
using BnbGo.Models.Security;

namespace BnbGo.Db
{
    
    public class ApplicationDbContextSeeder 
    {
        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = serviceProvider.GetService<ApplicationDbContext>()) 
            {
                // Random instance
                var random = new Random();

                // Currency
                if (!context.CurrencyTypes.Any())
                {
                    context.CurrencyTypes.AddRange(new List<CurrencyType>()
                    {
                        new CurrencyType { Name = "Euro", Description = "Euro"},
                        new CurrencyType { Name = "Dollar", Description = "Dollar"},
                    });
                    await context.SaveChangesAsync();
                }

                // Country
                if (!context.Countries.Any())
                {
                    context.Countries.AddRange(new List<Country>()
                    {
                        new Country { Name = "Belgium", Description = "European country", Iso2 = "BE", CurrencyTypeId = 1 }
                    });
                    await context.SaveChangesAsync();
                }

                // Region
                if (!context.Regions.Any())
                {
                    context.Regions.AddRange(new List<Region>()
                    {
                        new Region { Name = "Oost Vlaanderen", Description = "Oost Vlaanderen", CountryId =1 },
                        new Region { Name = "West Vlaanderen", Description = "West Vlaanderen", CountryId =1 },
                    });
                    await context.SaveChangesAsync();
                }

                // City
                if (!context.Cities.Any())
                {
                    context.Cities.AddRange(new List<City>()
                    {
                        new City { Name = "Gent", Description = "Gent", RegionId =1 },
                        new City { Name = "Brugge", Description = "Brugge", RegionId =2 },
                    });
                    await context.SaveChangesAsync();
                }

                // HouseType
                if (!context.HouseTypes.Any())
                {
                    context.HouseTypes.AddRange(new List<HouseType>()
                    {
                        new HouseType { Name = "Villa", Description = "Villa"},
                    });
                    await context.SaveChangesAsync();
                }

                // RoomType
                if (!context.RoomTypes.Any())
                {
                    context.RoomTypes.AddRange(new List<RoomType>()
                    {
                        new RoomType { Name = "Single bed", Description = "Single bed", GuestAmount = 2, BedAmount = 1 },
                    });
                    await context.SaveChangesAsync();
                }

                // RentType
                if (!context.RentTypes.Any())
                {
                    context.RentTypes.AddRange(new List<RentType>()
                    {
                        new RentType { Name = "Private room", Description = "Private room" },
                    });
                    await context.SaveChangesAsync();
                }

                // RoomState
                if (!context.RoomStates.Any())
                {
                    context.RoomStates.AddRange(new List<RoomState>()
                    {
                        new RoomState { Name = "Available", Description = "Available and visible" },
                        new RoomState { Name = "Blocked", Description = "Blocked by admins" },
                        new RoomState { Name = "Pending", Description = "Pending to be approved" },
                    });
                    await context.SaveChangesAsync();
                }

                // Person
                /*
                if(!context.Users.Any()) 
                {
                    var personSkeleton = new Faker<BnbGo.Models.Security.ApplicationUser>()
                        .RuleFor(p => p.Id, f => Guid.NewGuid())
                        .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                        .RuleFor(p => p.SurName, f => f.Person.LastName)
                        .RuleFor(p => p.Email, f => f.Person.Email)
                        .RuleFor(p => p.DayOfBirth, f => GenerateDateTime(1970, 1987, 1, 13, 1, 32))
                        .RuleFor(p => p.Gender, f => f.PickRandom<GenderType>())
                        .FinishWith((f, p) =>
                        {
                            Console.WriteLine("User Created! Id={0}", p.Id);
                        });
                        
                    var persons = new List<BnbGo.Models.Security.ApplicationUser>();
                    for(var i = 0;i<50;i++) {
                        var person = personSkeleton.Generate();
                        persons.Add(person);
                    }
                    context.Users.AddRange(persons);
                    await context.SaveChangesAsync();

                }
                */

                // Rooms
                /*
                if(!context.Rooms.Any()) 
                {
                    var lorem = new Bogus.DataSets.Lorem(locale: "en");
                    // Rooms
                    var roomSkeleton = new Faker<BnbGo.Models.Room>()
                        .RuleFor(r => r.Name, f => String.Join(" ", lorem.Words(2)))
                        .RuleFor(r => r.Description, f => String.Join(" ", lorem.Words(10)))
                        .RuleFor(r => r.HouseRules, f => String.Join(" ", lorem.Words(50)))
                        .FinishWith((f, r) =>
                        {
                            Console.WriteLine("Room Created! Id={0}", r.Id);
                        });
                        
                    var rooms = new List<BnbGo.Models.Room>();
                    for(var i = 0;i<50;i++) {
                        var room = roomSkeleton.Generate();
                        room.PriceBase = random.Next(25,300);
                        room.PriceExtraPerPerson = random.Next(0,200);
                        room.PricePerNight = random.Next(10,150);
                        room.HouseTypeId = 1;
                        room.RoomTypeId = 1;
                        room.RentTypeId = 1;
                        room.RoomStateId = 1;
                        room.CityId = 1;
                        rooms.Add(room);
                    }
                    context.Rooms.AddRange(rooms);
                    await context.SaveChangesAsync();

                }
                */

                // Reservations
                /*
                if(!context.Reservations.Any()) 
                {
                    var lorem = new Bogus.DataSets.Lorem(locale: "en");
                    // Reservations
                    var reservationSkeleton = new Faker<BnbGo.Models.Reservation>()
                        .RuleFor(r => r.Name, f => String.Join(" ", lorem.Words(2)))
                        .RuleFor(r => r.Description, f => String.Join(" ", lorem.Words(10)))
                        .RuleFor(r => r.Arrival, f => GenerateDateTime(2017, 2017, 1, 6, 1, 32))
                        .RuleFor(r => r.Departure, f => GenerateDateTime(2017, 2017, 7, 13, 1, 32))
                        .FinishWith((f, r) =>
                        {
                            Console.WriteLine("Reservation Created! Id={0}", r.Id);
                        });
                        
                    var reservations = new List<BnbGo.Models.Reservation>();
                    for(var i = 0;i<50;i++) {
                        var reservation = reservationSkeleton.Generate();
                        reservation.RoomId = random.Next(4,50);
                        reservation.PriceTotal = 1;
                        reservation.AmountOfGuests = 1;
                        reservations.Add(reservation);
                    }
                    context.Reservations.AddRange(reservations);
                    await context.SaveChangesAsync();

                }
                */

            }
        }

        private static DateTime GenerateDateTime(int yFrom, int yTo, int mFrom, int mTo, int dFrom, int dTo) {
            var random = new Random();
            try
            {
                return new DateTime(random.Next(yFrom, yTo), random.Next(mFrom, mTo), random.Next(dFrom, dTo));
            }
            catch(Exception ex) {
                return GenerateDateTime(yFrom, yTo, mFrom, mTo, dFrom, dTo);
            }
        }
    }
}