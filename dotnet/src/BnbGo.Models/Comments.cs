using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class Comment : BaseEntity<Int64>
    {
        // link with user
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        // link with room
        public Int64 RoomId { get; set; }
        public Room Room { get; set; }
        // Comment
        public string Content { get; set; }
    }
}