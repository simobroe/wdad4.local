using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class RatingType : BaseEntity<Int32>
    {
        public List<Rating> Ratings { get; set; }
    }
}