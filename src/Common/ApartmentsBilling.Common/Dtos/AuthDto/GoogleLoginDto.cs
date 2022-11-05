namespace ApartmentsBilling.Common.Dtos.AuthDto
{
    public class GoogleLoginDto
    {
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }

    }
}
