using System;

namespace ApartmentsBilling.Common.Dtos.BillsDto
{
    public class CreateBillDto
    {
        public Guid BilltypeId { get; set; }
        public Guid ApartmentId { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public float TotalPrice { get; set; }
    }
}
