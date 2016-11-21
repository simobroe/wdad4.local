using System;

namespace BnbGo.Models
{
    // BaseEntity implements the interface
    public class BaseEntity<T> : IBaseEntity<T>
    {
        // public means all classes have acces
        public T Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
    }
}