using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class Country : BaseEntity<Int32>
    {
        // country specific information
        public string Iso2 { get; set; }
        // currency for this country
        public Int16 CurrencyTypeId { get; set; }
        public CurrencyType CurrencyType { get; set; }
        // list of regions in this country
        public List<Region> Regions { get; set; }
        // list of users
        public List<ApplicationUser> Users { get; set; }
    }
}