using System;

namespace ApartmentsBilling.Common.Dtos.VehicleDto
{
    public class UpdateVehicleDto
    {
        public Guid Id { get; set; }
        public string PlateNumber { get; set; }

        public string VehicleType { get; set; }
    }
}
