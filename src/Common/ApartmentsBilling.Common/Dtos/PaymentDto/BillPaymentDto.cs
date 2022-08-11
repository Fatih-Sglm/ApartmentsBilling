using System;

namespace ApartmentsBilling.Common.Dtos.PaymentDto
{
    public class BillPaymentDto
    {
        public Guid BillId { get; set; }
        public string CardNumber { get; set; }

        public string LastUseDate { get; set; }

        public int Cv2 { get; set; }
    }
}
