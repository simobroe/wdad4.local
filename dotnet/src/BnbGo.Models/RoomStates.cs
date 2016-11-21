using System;
using System.Collections.Generic;

namespace BnbGo.Models
{
    public class RoomState : BaseEntity<Int32>
    {
        // link with Room
        public List<Room> Rooms { get; set; }
    }
}
