using ApartmentsBilling.Entity.Entities.Common;
using System;

namespace ApartmentsBilling.Entity.Entities
{
    public class Message : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public bool IsRead { get; set; }
    }
}
