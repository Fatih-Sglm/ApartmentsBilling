namespace ApartmentsBilling.BussinessLayer.Configuration.Extension
{
    public class EntityEnums
    {
        public static string Func(string data)
        {
            switch (data)
            {
                case "User":
                    return "Kullanıcı";

                case "Vehicle":
                    return "Araç";

                case "Bill":
                    return "Fatura";

                case "BillType":
                    return "Fatura Tipi";

                case "Flat":
                    return "Daire";

                case "Apartment":
                    return "Apartman";

                case "Message":
                    return "Mesaj";

                case "Receipt":
                    return "Fiş";

                default:
                    return null;

            }
        }
    }
}
