using ApartmentsBilling.Entity.Entities.Common;
using System;
using System.Collections.Generic;

namespace ApartmentsBilling.Entity.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public Flat Flat { get; set; }
        public Guid FlatId { get; set; }
        public UserPassword Password { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Message> Messages { get; set; }
        public UserRole Role { get; set; } = UserRole.User;



    }
}
