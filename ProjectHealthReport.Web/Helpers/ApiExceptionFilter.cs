using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectHealthReport.Web.Models;

namespace ProjectHealthReport.Web.Helpers
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var error = ExtractErrorResponseFromContext(context);

            context.HttpContext.Response.StatusCode = (int) error.HttpStatusCode;
            context.Result = new JsonResult(error);
        }

        private static ErrorResponse ExtractErrorResponseFromContext(ExceptionContext context)
        {
            return context.Exception switch
            {
                AutoMapperMappingException mappingException => mappingException.CreateErrorResponse(),
                // CompositeValidationException compositeValidationException => compositeValidationException.CreateErrorResponse(),
                // ValidationException validationException => validationException.CreateErrorResponse(),
                // AuthorizationException authorizationException => authorizationException.CreateErrorResponse(),
                _ => context.Exception.CreateErrorResponse()
            };
        }
    }
}