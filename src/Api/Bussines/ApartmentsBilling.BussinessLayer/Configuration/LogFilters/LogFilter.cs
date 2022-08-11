using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace ApartmentsBilling.BussinessLayer.Configuration.LogFilters
{
    public class LogFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var mssLogger = context.HttpContext.RequestServices.GetService<MssqlLog>();
            mssLogger._logger.Information(JsonSerializer.Serialize(context.ActionArguments));
        }
    }
}
