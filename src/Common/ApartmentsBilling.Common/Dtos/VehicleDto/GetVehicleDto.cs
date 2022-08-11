using System;

namespace ApartmentsBilling.Common.Dtos.VehicleDto
{
    public class GetVehicleDto
    {
        public Guid Id { get; set; }
        public string PlateNumber { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public string VehicleType { get; set; }
    }
}
