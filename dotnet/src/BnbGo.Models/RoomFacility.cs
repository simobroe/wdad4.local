using System;
using System.Collections.Generic;

namespace BnbGo.Models
{
    public class RoomFacility
    {
        public Int64 RoomId { get; set; }
        public Room Room { get; set; }

        public Int64 FacilityId { get; set; }
        public Facility Facility { get; set; }
    }
}
