using ApartmentsBilling.BussinessLayer.Configuration.Exceptions;
using ApartmentsBilling.BussinessLayer.Configuration.LogFilters;
using ApartmentsBilling.Common.Dtos.CustomDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace ApartmentsBilling.BussinessLayer.Configuration.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //var logger = context.HttpContext.RequestServices.GetService<MssqlLog>();
            //var response = context.Exception.Message;
            //var error = new ErrorDto()
            //{
            //    ErrorMessage = new List<string> { context.Exception.Message },
            //};
            //var ExeptionResult = context.Exception switch
            //{
            //    ClientSideException => new BadRequestObjectResult(JsonSerializer.Serialize(error)), /*CustomResponseDto<NoContent>.Fail(response))*/
            //    NotFoundException => new NotFoundObjectResult(JsonSerializer.Serialize(error)),
            //    _ => context.Result = new StatusCodeResult(500)
            //};
            //var error = new ErrorDto()
            //{
            //    ErrorMessage = new List<string> { context.Exception.Message },
            //    StatusCode = context.Exception switch
            //    {
            //        ClientSideException => 400,
            //        NotFoundException => 404,
            //        _ => 500
            //    }


            //};

            var logger = context.HttpContext.RequestServices.GetService<MssqlLog>();
            var response = context.Exception.Message;
            var errors = JsonSerializer.Serialize(CustomResponseDto<NoContent>.Fail(response));
            var ExeptionResult = context.Exception switch
            {
                ClientSideException => new BadRequestObjectResult(errors),
                NotFoundException => new NotFoundObjectResult(errors),
                _ => context.Result = new StatusCodeObjectResult(500, errors),
            };
            if (context.Exception.GetType() != typeof(ClientSideException))
                logger._logger.Error(new JsonResult(new { error = context.Exception.Message, context.Exception.InnerException, }).Value.ToString());
            context.Result = ExeptionResult;
            //context.Result = new ObjectResult(JsonSerializer.Serialize(error));
            base.OnException(context);
        }
    }
}
