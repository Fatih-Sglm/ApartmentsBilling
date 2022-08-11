using System;

namespace ApartmentsBilling.Common.Dtos.FlatDto
{
    public class UpdateFlatDto
    {
        public Guid Id { get; set; }
        public Guid ApartmentId { get; set; }
        public string WhichBlock { get; set; }
        public bool IsEmpty { get; set; } = true;
        public string FloorType { get; set; }
        public ushort FloorLocation { get; set; }
        public ushort FloorNumber { get; set; }
        public bool IsRented { get; set; }
    }
}
