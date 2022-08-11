using System;

namespace ApartmentsBilling.BussinessLayer.Configuration.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
