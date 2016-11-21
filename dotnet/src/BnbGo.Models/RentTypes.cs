using System;
using System.Collections.Generic;

namespace BnbGo.Models
{
    public class RentType : BaseEntity<Int32>
    {
        // rooms from this rent type
        public List<Room> Rooms { get; set; }
    }
}
