using System;

namespace ApartmentsBilling.Common.Dtos.BillsDto
{
    public class BillDto
    {
        public Guid Id { get; set; }
        public string BillOwner { get; set; }
        public float Price { get; set; }
        public string BillType { get; set; }
        public string FloorNumber { get; set; }
        public bool IsPayment { get; set; }

        public DateTime LastPaymentDate { get; set; }

    }
}
