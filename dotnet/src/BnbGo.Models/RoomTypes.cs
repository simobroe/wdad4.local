using System;
using System.Collections.Generic;

namespace BnbGo.Models
{
    public class RoomType : BaseEntity<Int32>
    {
        public Int16 BedAmount { get; set; }
        public Int16 GuestAmount { get; set; }
        // link with Room
        public List<Room> Rooms { get; set; }
    }
}
