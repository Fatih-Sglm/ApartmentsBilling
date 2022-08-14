using Microsoft.AspNetCore.Mvc;

namespace ApartmentsBilling.BussinessLayer.Configuration.Filter
{
    public class StatusCodeObjectResult : ObjectResult
    {
        public StatusCodeObjectResult(int statusCode, object value)
            : base(value)
        {
            StatusCode = statusCode;
        }
    }
}