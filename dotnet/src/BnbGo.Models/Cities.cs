using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class City : BaseEntity<Int64>
    {
        // postal code
        public string Postal { get; set; }
        // link with region
        public Int64 RegionId { get; set; }
        public Region Region { get; set; }
        // list with rooms in this city
        public List<Room> Rooms { get; set; }
        // list with users in this city
        public List<ApplicationUser> Users { get; set; }
    }
}