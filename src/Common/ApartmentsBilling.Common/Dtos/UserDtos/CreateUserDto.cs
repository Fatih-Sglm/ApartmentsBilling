using ApartmentsBilling.Entity.Entities;
using System;

namespace ApartmentsBilling.Common.Dtos.UserDtos
{
    public class CreateUserDto
    {
        public Guid FlatId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
    }
}
