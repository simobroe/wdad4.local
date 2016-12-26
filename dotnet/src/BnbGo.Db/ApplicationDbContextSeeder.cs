using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions;
using BnbGo.Models;
using BnbGo.Models.Security;
using Newtonsoft.Json.Linq;

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

                // user id's
                List<Guid> UserIds = new List<Guid>();

                // Currency
                if (!context.CurrencyTypes.Any())
                {
                    context.CurrencyTypes.AddRange(new List<CurrencyType>()
                    {
                        new CurrencyType { Name = "Euro", Description = "€"},
                        new CurrencyType { Name = "Dollar", Description = "$"},
                        new CurrencyType { Name = "Yen", Description = "¥"},
                        new CurrencyType { Name = "Pound", Description = "`£"},
                    });
                    await context.SaveChangesAsync();
                }

                // Country
                if (!context.Countries.Any())
                {
                    string json = File.ReadAllText("../BnbGo.Db/countries.json");
                    dynamic countries = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    List<Country> countryList = new List<Country>();
                    
                    var countriesInJson = countries.country;
                    foreach (var countryInJson in countriesInJson)
                    {
                        short currentCurrency = 1;
                        switch ((string)countryInJson.Description)
                        {
                            case "EUR":
                                currentCurrency = 1;
                                break;
                            case "USD":
                                currentCurrency = 2;
                                break;
                            case "JPY":
                                currentCurrency = 3;
                                break;
                            case "GBP":
                                currentCurrency = 4;
                                break;
                            default:
                                currentCurrency = 1;
                                break;
                        };
                        countryList.Add(new Country() {
                            Name = countryInJson.Name,
                            Description = countryInJson.Description,
                            Iso2 = countryInJson.Iso2,
                            CurrencyTypeId = currentCurrency
                        });
                    }

                    context.Countries.AddRange(countryList);

                    await context.SaveChangesAsync();
                }
                
                // Region
                if (!context.Regions.Any())
                {
                    context.Regions.AddRange(new List<Region>()
                    {
                        // Belgium
                        new Region { Name = "Oost Vlaanderen", Description = "Oost Vlaanderen", CountryId =176 },
                        new Region { Name = "West Vlaanderen", Description = "West Vlaanderen", CountryId =176 },
                        new Region { Name = "Antwerpen", Description = "Antwerpen", CountryId =176 },
                        new Region { Name = "Limburg", Description = "Limburg", CountryId =176 },
                        new Region { Name = "Vlaams Brabant", Description = "Vlaams Brabant", CountryId =176 },
                        new Region { Name = "Waals Brabant", Description = "Waals Brabant", CountryId =176 },
                        new Region { Name = "Henegouwen", Description = "Henegouwen", CountryId =176 },
                        new Region { Name = "Luik", Description = "Luik", CountryId =176 },
                        new Region { Name = "Luxemburg", Description = "Luxemburg", CountryId =176 },
                        new Region { Name = "Namen", Description = "Namen", CountryId =176 },
                        // Netherlands
                        new Region { Name = "Groningen", Description = "Groningen", CountryId =9 },
                        new Region { Name = "Friesland", Description = "Friesland", CountryId =9 },
                        new Region { Name = "Drenthe", Description = "Drenthe", CountryId =9 },
                        new Region { Name = "Overijssel", Description = "Overijssel", CountryId =9 },
                        new Region { Name = "Flevoland", Description = "Flevoland", CountryId =9 },
                        new Region { Name = "Gelderland", Description = "Gelderland", CountryId =9 },
                        new Region { Name = "Utrecht", Description = "Utrecht", CountryId =9 },
                        new Region { Name = "Noord-Holland", Description = "Noord-Holland", CountryId =9 },
                        new Region { Name = "Zuid-Holland", Description = "Zuid-Holland", CountryId =9 },
                        new Region { Name = "Zeeland", Description = "Zeeland", CountryId =9 },
                        new Region { Name = "Noord-Brabant", Description = "Noord-Brabant", CountryId =9 },
                        new Region { Name = "Limburg", Description = "Limburg", CountryId =9 },
                        // France
                        new Region { Name = "Normandië", Description = "Normandië", CountryId =231 },
                        new Region { Name = "Bourgondië", Description = "Bourgondië", CountryId =231 },
                        new Region { Name = "Provence", Description = "Provence", CountryId =231 },
                        new Region { Name = "Île-de-france", Description = "Île-de-france", CountryId =231 },
                    });
                    await context.SaveChangesAsync();
                }
                
                // City
                if (!context.Cities.Any())
                {
                    context.Cities.AddRange(new List<City>()
                    {
                        new City { Name = "Gent", Description = "Gent", RegionId =1, Postal = "9000" },
                        new City { Name = "Aalter", Description = "Aalter", RegionId =1, Postal = "9880" },
                        new City { Name = "Nevele", Description = "Nevele", RegionId =1, Postal = "9850" },
                        new City { Name = "Mariakerke", Description = "Mariakerke", RegionId =1, Postal = "9030" },
                        new City { Name = "Drongen", Description = "Drongen", RegionId =1, Postal = "9031" },
                        new City { Name = "Brugge", Description = "Brugge", RegionId =24, Postal = "8000" },
                        new City { Name = "Poperinge", Description = "Poperinge", RegionId =24, Postal = "8970" },
                        new City { Name = "Parijs", Description = "Parijs", RegionId =26, Postal = "57000" },
                        new City { Name = "Amsterdam", Description = "Amsterdam", RegionId =8, Postal = "1000" },
                    });
                    await context.SaveChangesAsync();
                }
                
                // HouseType
                if (!context.HouseTypes.Any())
                {
                    context.HouseTypes.AddRange(new List<HouseType>()
                    {
                        new HouseType { Name = "Log Cabin", Description = "Log Cabin"},
                        new HouseType { Name = "Farm", Description = "Farm"},
                        new HouseType { Name = "Tree house", Description = "Tree house"},
                        new HouseType { Name = "Bungalowe", Description = "Bungalow"},
                        new HouseType { Name = "Iglo", Description = "Iglo"},
                        new HouseType { Name = "Caravan", Description = "Caravan"},
                        new HouseType { Name = "Cottage", Description = "Cottage"},
                        new HouseType { Name = "Cave house", Description = "Cave house"},
                        new HouseType { Name = "Palace", Description = "Palace"},
                        new HouseType { Name = "Church", Description = "Church"},
                        new HouseType { Name = "Tent", Description = "Tent"},
                        new HouseType { Name = "Villa", Description = "Villa"},
                        new HouseType { Name = "Detached house", Description = "Detached house"},
                        new HouseType { Name = "Semi detached house", Description = "Semi detached house"},
                        new HouseType { Name = "Attached house", Description = "Attached house"},
                    });
                    await context.SaveChangesAsync();
                }

                // RoomType
                if (!context.RoomTypes.Any())
                {
                    context.RoomTypes.AddRange(new List<RoomType>()
                    {
                        new RoomType { Name = "1 Single bed", Description = "Single bed", GuestAmount = 2, BedAmount = 1 },
                        new RoomType { Name = "2 Single beds", Description = "Single bed", GuestAmount = 2, BedAmount = 2 },
                        new RoomType { Name = "1 Double bed", Description = "Double bed", GuestAmount = 2, BedAmount = 1 },
                        new RoomType { Name = "1 Double bed + 1 kid bed", Description = "Double bed + kid bed", GuestAmount = 3, BedAmount = 2 },
                    });
                    await context.SaveChangesAsync();
                }

                // RentType
                if (!context.RentTypes.Any())
                {
                    context.RentTypes.AddRange(new List<RentType>()
                    {
                        new RentType { Name = "Private room", Description = "Private room" },
                        new RentType { Name = "Shared room", Description = "Shared room" },
                        new RentType { Name = "Whole house", Description = "Whole house" },
                        new RentType { Name = "Shared house", Description = "Shared house" },
                    });
                    await context.SaveChangesAsync();
                }

                // RoomState
                if (!context.RoomStates.Any())
                {
                    context.RoomStates.AddRange(new List<RoomState>()
                    {
                        new RoomState { Name = "Confirmed", Description = "Confirmed and visible" },
                        new RoomState { Name = "Canceled", Description = "Canceled by admins" },
                        new RoomState { Name = "Pending", Description = "Pending to be approved" },
                    });
                    await context.SaveChangesAsync();
                }

                // Facility
                if (!context.Facilities.Any())
                {
                    context.Facilities.AddRange(new List<Facility>()
                    {
                        new Facility { Name = "Kitchen", Description = "make your own meal" },
                        new Facility { Name = "Television", Description = "your own television on your room" },
                        new Facility { Name = "Internet", Description = "internet through cable" },
                        new Facility { Name = "Wifi", Description = "wireless internet" }
                    });
                    await context.SaveChangesAsync();
                }

                // RatingType
                if (!context.RatingTypes.Any())
                {
                    context.RatingTypes.AddRange(new List<RatingType>()
                    {
                        new RatingType { Name = "Excelent", Description = "Excelent" },
                        new RatingType { Name = "Good", Description = "Good" },
                        new RatingType { Name = "Semi", Description = "Semi" },
                        new RatingType { Name = "Bad", Description = "Bad" }
                    });
                    await context.SaveChangesAsync();
                }

                
                // Users
                if(!context.Users.Any()) {
                    var personSkeleton = new Faker<ApplicationUser>()
                        .RuleFor(p => p.Id, f => Guid.NewGuid())
                        .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                        .RuleFor(p => p.SurName, f => f.Name.LastName())
                        .RuleFor(p => p.UserName, (f, p) => f.Internet.UserName(p.FirstName, p.SurName))
                        .RuleFor(p => p.Email, f => f.Person.Email)
                        .RuleFor(p => p.DayOfBirth, f => GenerateDateTime(1970, 1987, 1, 13, 1, 32))
                        .RuleFor(p => p.Gender, f => f.PickRandom<GenderType>())
                        .FinishWith((f, p) =>
                        {
                            Console.WriteLine("User Created! Name={0}", p.UserName);
                            UserIds.Add(p.Id);
                        });
                        
                    var persons = new List<ApplicationUser>();
                    for(var i = 0;i<5;i++) {
                        var person = personSkeleton.Generate();
                        var randomCountry = random.Next(1,3);
                        person.PlainPassword = GenerateAlwaysTheSamePassword();
                        switch (randomCountry)
                        {
                            case 1:
                                person.CountryId = 176;
                                person.RegionId = 1;
                                person.CityId = 1;
                                break;
                            case 2:
                                person.CountryId = 9;
                                person.RegionId = 8;
                                person.CityId = 9;
                                break;
                            case 3:
                                person.CountryId = 231;
                                person.RegionId = 26;
                                person.CityId = 8;
                                break;
                        }
                        persons.Add(person);
                    }

                    context.Users.AddRange(persons);
                    await context.SaveChangesAsync();
                }

                // Locations
                if (!context.Locations.Any())
                {
                    context.Locations.AddRange(new List<Location>()
                    {
                        new Location { Name = "Tomato street", Description = "22", CityId =random.Next(1,10), },
                        new Location { Name = "Wall street", Description = "8", CityId =random.Next(1,10), },
                        new Location { Name = "Beach street", Description = "12", CityId =random.Next(1,10), },
                        new Location { Name = "Railway street", Description = "38", CityId =random.Next(1,10), },
                        new Location { Name = "Northwood street", Description = "19A", CityId =random.Next(1,10), },
                        new Location { Name = "Tango street", Description = "27", CityId =random.Next(1,10), },
                        new Location { Name = "Liberty street", Description = "39", CityId =random.Next(1,10), },
                        new Location { Name = "Industry street", Description = "1", CityId =random.Next(1,10), },
                        new Location { Name = "Samson street", Description = "3", CityId =random.Next(1,10), },
                    });
                    await context.SaveChangesAsync();
                }

                // Rooms
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
                    for(var i = 0;i<12;i++) {
                        var room = roomSkeleton.Generate();
                        room.PriceBase = random.Next(25,300);
                        room.PriceExtraPerPerson = random.Next(0,200);
                        room.PricePerNight = random.Next(10,150);
                        room.HouseTypeId = random.Next(1,16);
                        room.RoomTypeId = random.Next(1,5);
                        room.RentTypeId = random.Next(1,5);
                        room.RoomStateId = random.Next(1,4);
                        room.UserId = (Guid)UserIds[random.Next(UserIds.Count)];
                        room.CityId = random.Next(1,10);
                        room.LocationId = random.Next(1,10);
                        rooms.Add(room);
                    }
                    context.Rooms.AddRange(rooms);
                    await context.SaveChangesAsync();

                }

                // Reservations
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
                    for(var i = 0;i<8;i++) {
                        var reservation = reservationSkeleton.Generate();
                        reservation.RoomId = random.Next(1,12);
                        reservation.PriceTotal = random.Next(75,185);
                        reservation.AmountOfGuests = random.Next(1,3);
                        reservation.UserId = (Guid)UserIds[random.Next(UserIds.Count)];
                        reservations.Add(reservation);
                    }
                    context.Reservations.AddRange(reservations);
                    await context.SaveChangesAsync();

                }

                // Rooms and Facilities
                if (!context.RoomFacilities.Any())
                {
                    List<RoomFacility> roomfacilities = new List<RoomFacility>();

                    for (var i = 1; i < 12; i++)
                    {
                        var facilityAmount = random.Next(1,4);

                        switch (facilityAmount)
                        {
                            case 1:
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 1 });
                                break;
                            case 2:
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 1 });
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 2 });
                                break;
                            case 3:
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 1 });
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 2 });
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 3 });
                                break;
                            case 4:
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 1 });
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 2 });
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 3 });
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 4 });
                                break;
                            default:
                                roomfacilities.Add(new RoomFacility() { RoomId = i, FacilityId = 1 });
                                break;
                        }
                    }

                    context.RoomFacilities.AddRange(roomfacilities);

                    await context.SaveChangesAsync();
                }

                // Image types
                if (!context.ImageTypes.Any())
                {
                    context.ImageTypes.AddRange(new List<ImageType>()
                    {
                        new ImageType { Name = "User", Description = "User" },
                        new ImageType { Name = "Room", Description = "Room" },
                        new ImageType { Name = "Region", Description = "Region" }
                    });
                    await context.SaveChangesAsync();
                }
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

        private static string GenerateAlwaysTheSamePassword()
        {
            return "bnbgo";
        }

    }
}