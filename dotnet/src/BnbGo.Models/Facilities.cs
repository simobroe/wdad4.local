using System;
using System.Collections.Generic;

namespace BnbGo.Models
{
    public class Facility : BaseEntity<Int64>
    {
        // list of rooms with facility
        public List<RoomFacility> Rooms { get; set; }
    }
}
