using System;

namespace ApartmentsBilling.Common.Dtos.PaymentDto
{

    public class CreateBillReceiptDto
    {
        public Guid BillId { get; set; }
        public int PaymentNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string BillType { get; set; }
        public float Total { get; set; }
    }
}
