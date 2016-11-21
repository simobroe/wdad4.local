using System;

namespace BnbGo.Models
{
    // T wordt later vervangen door het datatype 
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DateTime CreatedAt { get; set; }
        // deze eigenschappen zijn optioneel
        Nullable<DateTime> UpdatedAt { get; set; }
        Nullable<DateTime> DeletedAt { get; set; }
    }
}