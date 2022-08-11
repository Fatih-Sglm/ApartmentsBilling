using ApartmentsBilling.Entity.Entities.Common;
using System.Collections.Generic;

namespace ApartmentsBilling.Entity.Entities
{
    public class BillType : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Bill> Bills { get; set; }
    }
}
