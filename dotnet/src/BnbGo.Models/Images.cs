using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class Image : BaseEntity<Int32>
    {
        // image url
        public string Link { get; set; }
        // link with room
        public Int64 RoomId { get; set; }
        public Room Room { get; set; }
        // link with user
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        // link with image type
        public ImageType ImageType { get; set; }
    }
}