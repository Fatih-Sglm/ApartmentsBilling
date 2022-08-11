using System;

namespace ApartmentsBilling.Entity.Entities.Common
{
    public abstract class BaseEntity
    {

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }
    }
}
