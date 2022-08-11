using ApartmentsBilling.Entity.Entities.Common;
using System;

namespace ApartmentsBilling.Entity.Entities
{
    public class Bill : BaseEntity
    {
        public float Price { get; set; }
        public BillType BillType { get; set; }
        public Guid BillTypeId { get; set; }
        public Flat Flat { get; set; }
        public Guid FlatId { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public bool IsPayment { get; set; }

    }
}
