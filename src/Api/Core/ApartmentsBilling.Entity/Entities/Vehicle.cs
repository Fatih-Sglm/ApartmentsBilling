using ApartmentsBilling.Entity.Entities.Common;
using System;

namespace ApartmentsBilling.Entity.Entities
{
    public class Vehicle : BaseEntity
    {
        public string PlateNumber { get; set; }

        public string VehicleType { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
