using ApartmentsBilling.Entity.Entities.Common;
using System;

namespace ApartmentsBilling.Entity.Entities
{
    public class UserPassword : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
