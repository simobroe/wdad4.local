using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class Region : BaseEntity<Int32>
    {
        // link with images
        public List<Image> Images { get; set; }
        // link with country
        public Int32 CountryId { get; set; }
        public Country Country { get; set; }
        // list of cities
        public List<City> Cities { get; set; }
        // list of users
        public List<ApplicationUser> Users { get; set; }
    }
}