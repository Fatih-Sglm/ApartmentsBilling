namespace ApartmentsBilling.Common.Dtos.AdminDto
{
    public class CreateAdminDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string ApartmentName { get; set; }
        public string WhichBlock { get; set; }
        public string FloorType { get; set; }
        public ushort FloorLocation { get; set; }
        public ushort FloorNumber { get; set; }
        public bool IsRented { get; set; }

    }
}
