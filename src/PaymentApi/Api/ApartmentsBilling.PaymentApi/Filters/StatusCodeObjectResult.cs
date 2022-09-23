using Microsoft.AspNetCore.Mvc;

namespace ApartmentsBilling.PaymentApi.Filters
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