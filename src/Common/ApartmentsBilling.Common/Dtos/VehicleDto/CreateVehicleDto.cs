using System;

namespace ApartmentsBilling.Common.Dtos.VehicleDto
{
    public class CreateVehicleDto
    {

        public string PlateNumber { get; set; }
        public string VehicleType { get; set; }
        public Guid UserId { get; set; }

    }
}
