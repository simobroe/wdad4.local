using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class Location : BaseEntity<Int32>
    {
        // location specific information
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        // link with room
        public Int64 RoomId { get; set; }
        public Room Room { get; set; }
    }
}