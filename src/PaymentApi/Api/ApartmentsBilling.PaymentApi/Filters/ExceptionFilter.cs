using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.PaymentApiSevices.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ApartmentsBilling.PaymentApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var response = context.Exception.Message;
            var errors = JsonSerializer.Serialize(CustomResponseDto<NoContent>.Fail(response));
            var ExeptionResult = context.Exception switch
            {
                ClientSideException => new BadRequestObjectResult(errors),
                NotFoundException => new NotFoundObjectResult(errors),
                _ => context.Result = new StatusCodeObjectResult(500, errors),
            };
            context.Result = ExeptionResult;
            base.OnException(context);
        }
    }
}
