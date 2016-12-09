using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class Location : BaseEntity<Int32>
    {
        // link with room
        public List<Room> Rooms { get; set; }
        // link with city
        public Int64 CityId { get; set; }
        public City City { get; set; }
    }
}