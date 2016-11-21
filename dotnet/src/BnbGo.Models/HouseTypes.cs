using System;
using System.Collections.Generic;

namespace BnbGo.Models
{
    public class HouseType : BaseEntity<Int32>
    {
        // rooms from this housetype
        public List<Room> Rooms { get; set; }
    }
}
