namespace ApartmentsBilling.PaymentApiSevices.DBSettings
{
    public interface IDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
