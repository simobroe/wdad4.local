using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class Rating : BaseEntity<Int64>
    {
        // link with user
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        // link with room
        public Int64 RoomId { get; set; }
        public Room Room { get; set; }
        // rating type
        public Int32 RatingTypeId { get; set; }
        public RatingType RatingType { get; set; }
    }
}