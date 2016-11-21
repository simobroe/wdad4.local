using System;
using System.Collections.Generic;

namespace BnbGo.Models
{
    public class ReservationDay : BaseEntity<Int64>
    {
        // link with room
        public Int64 RoomId { get; set; }
        public Room Room { get; set; }
        // link with reservation
        public Int64 ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        // reservation day specific information
        public DateTime Day { get; set; }
    }
}