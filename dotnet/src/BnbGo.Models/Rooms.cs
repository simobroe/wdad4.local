using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class Room : BaseEntity<Int64>
    {
        // room specific information
        public string HouseRules { get; set;}
        public Int32 PriceBase { get; set; }
        public Int32 PricePerNight { get; set; }
        public Int32 PriceExtraPerPerson { get; set; }
        // link with types
        public Int32 RentTypeId { get; set; }
        public RentType RentType { get; set; }
        public Int32 HouseTypeId { get; set; }
        public HouseType HouseType { get; set; }
        public Int32 RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        // link with images
        public List<Image> Images { get; set; }
        // link with city
        public Int64 CityId { get; set; }
        public City City { get; set; }
        // link with landlord
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        // link with location
        public Int32 LocationId { get; set; }
        public Location Location { get; set; }
        // rooom state
        public Int32 RoomStateId { get; set; }
        public RoomState RoomState { get; set; }
        // all reservations
        public List<Reservation> Reservations { get; set; }
        // all reservation days
        public List<ReservationDay> ReservationDays { get; set; }
        // facilities
        public List<RoomFacility> Facilities { get; set; }
        // comments
        public List<Comment> Comments { get; set; }
        // ratings
        public List<Rating> Ratings { get; set; }
    }
}