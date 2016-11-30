using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OpenIddict;

namespace BnbGo.Models.Security
{
    public enum GenderType : byte {
        Unknown = 0,
        Male = 1,
        Female = 2,
        NotApplicable = 9
    }

    public class ApplicationUser : IdentityUser<Guid> 
    {
        // basic user information
        public String FirstName { get; set; }
        public String SurName { get; set; }
        public String PlainPassword  { get; set; }
        public GenderType Gender { get; set; }
        public Nullable<DateTime> DayOfBirth { get; set; }
        // link with country
        public Int32 CountryId { get; set; }
        public Country Country { get; set; }
        // link with region
        public Int32 RegionId { get; set; }
        public Region Region { get; set; }
        // link with city
        public Int64 CityId { get; set; }
        public City City { get; set; }
        // link with image
        public Int64 ImageId { get; set; }
        public Image Image { get; set; }
        // if user is a renter or landlord
        public List<Room> Rooms { get; set; }
        public List<Reservation> Reservations { get; set; }
        // basic Create Update and Delete
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        // user Comments
        public List<Comment> Comments { get; set; }
        // ratings
        public List<Rating> Ratings { get; set; }
    }
}