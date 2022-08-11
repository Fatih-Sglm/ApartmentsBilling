using ApartmentsBilling.PaymentApiSevices.Entities.Common;

namespace ApartmentsBilling.PaymentApiSevices.Entities
{
    public class Receipt : BaseEntity
    {
        public string BillId { get; set; }
        public int PaymentNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string BillType { get; set; }
        public float Total { get; set; }
    }
}
