using System;

namespace ApartmentsBilling.BussinessLayer.Configuration.Exceptions
{
    public class ClientSideException : Exception
    {
        public ClientSideException(string message) : base(message)
        {
        }
        public ClientSideException(string type, Exception exception) : base(type, exception)
        {

        }
    }
}
