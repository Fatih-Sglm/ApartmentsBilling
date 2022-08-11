namespace ApartmentsBilling.Common.Dtos.UserDtos
{
    public class GetUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public ushort FloorNumber { get; set; }
        public int VehicleCount { get; set; }
    }
}
