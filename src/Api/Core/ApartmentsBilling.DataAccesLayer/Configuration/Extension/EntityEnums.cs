namespace ApartmentsBilling.DataAccesLayer.Configuration.Extension
{
    public class EntityEnums
    {
        public string Func(string data)
        {
            return data switch
            {
                "User" => "Kullanıcı",
                "Vehicle" => "Araç",
                "Bill" => "Fatura",
                "BillType" => "Fatura Tipi",
                "Flat" => "Daire",
                "Apartment" => "Apartman",
                "Message" => "Mesaj",
                "Receipt" => "Fiş",
                _ => null,
            };
        }
    }
}
