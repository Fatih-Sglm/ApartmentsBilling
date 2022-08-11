using System;

namespace ApartmentsBilling.Common.Dtos.PaymentDto
{
    public class GetReceiptDto
    {
        public string Id { get; set; }
        public int PaymentNumber { get; set; }
        public string FullName { get; set; }
        public string BillType { get; set; }
        public string UserId { get; set; }
        public DateTime PaymentDate { get; set; }
        public float Total { get; set; }
    }
}
