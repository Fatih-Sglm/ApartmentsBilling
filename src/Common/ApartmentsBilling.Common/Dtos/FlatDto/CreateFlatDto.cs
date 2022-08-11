using System;

namespace ApartmentsBilling.Common.Dtos.FlatDto
{
    public class CreateFlatDto
    {
        public string WhichBlock { get; set; }
        public string FloorType { get; set; }
        public ushort FloorLocation { get; set; }
        public ushort FloorNumber { get; set; }
        public bool IsRented { get; set; } = true;
        public Guid ApartmentId { get; set; }
    }
}
