using System;
using System.Collections.Generic;

namespace BnbGo.Models
{
    public class CurrencyType : BaseEntity<Int16>
    {
        // list of countries with this currency
        public List<Country> Countries { get; set; }
    }
}