using ApartmentsBilling.Entity.Entities.Common;
using System.Collections.Generic;

namespace ApartmentsBilling.Entity.Entities
{
    public class Apartment : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Flat> Flats { get; set; }

    }
}
