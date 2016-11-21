using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class Reservation : BaseEntity<Int64>
    {
        // link with user
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        // link with room
        public Int64 RoomId { get; set; }
        public Room Room { get; set; }
        // reservation specific information
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public Int32 PriceTotal { get; set; }
        public Int32 AmountOfGuests { get; set; }
        // days in this reservation
        public List<ReservationDay> ReservationDays { get; set; }
    }
}