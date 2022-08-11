using System;

namespace ApartmentsBilling.Common.Dtos.FlatDto
{
    public class GetFlatDto
    {
        public Guid Id { get; set; }
        public string Tenant_name { get; set; }
        public string WhichBlock { get; set; }
        public bool isEmpty { get; set; }
        public string FloorType { get; set; }
        public ushort FloorLocation { get; set; }
        public ushort FloorNumber { get; set; }
        public bool isRented { get; set; }
    }
}
