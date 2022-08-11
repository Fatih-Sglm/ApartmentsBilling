namespace ApartmentsBilling.PaymentApiSevices.DBSettings
{
    public class DbSettings : IDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
