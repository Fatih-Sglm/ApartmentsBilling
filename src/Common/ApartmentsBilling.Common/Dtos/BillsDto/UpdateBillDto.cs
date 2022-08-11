using System;

namespace ApartmentsBilling.Common.Dtos.BillsDto
{
    public class UpdateBillDto
    {
        public Guid Id { get; set; }
        public float Price { get; set; }
        public Guid BillTypeId { get; set; }
        public Guid FlatId { get; set; }

        public DateTime LastPaymentDate { get; set; }
        public bool IsPayment { get; set; }
    }
}
