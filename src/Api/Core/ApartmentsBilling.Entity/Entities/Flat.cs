using ApartmentsBilling.Entity.Entities.Common;
using System;

namespace ApartmentsBilling.Entity.Entities
{
    public class Flat : BaseEntity
    {
        public string WhichBlock { get; set; }
        public bool IsEmpty { get; set; } = true;
        public string FloorType { get; set; }
        public ushort FloorLocation { get; set; }
        public ushort FloorNumber { get; set; }
        public bool IsRented { get; set; }
        public Apartment Apartment { get; set; }
        public Guid ApartmentId { get; set; }
        public User User { get; set; }
    }
}
