﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using AutoMapper;
 using ProjectHealthReport.Domains.Exceptions;
 using ProjectHealthReport.Web.Models;

namespace ProjectHealthReport.Web.Helpers
{
    public static class ExceptionExtensions
    {
        public static ErrorResponse CreateErrorResponse(this AutoMapperMappingException ex)
        {
            var error = "Error in mapping invalid data";
            var validationError = ex?.InnerException?.InnerException?.Message;

            if (validationError == null)
            {
                validationError = ex?.InnerException?.Message;
            }

            if (validationError != null)
            {
                error = error + " - " + validationError;
            }

            return new ErrorResponse(error, HttpStatusCode.BadRequest, null);
        }
        
        public static ErrorResponse CreateErrorResponse(this DomainException ex)
        {
            var error = $"{ex.Code} - {ex.Message}";

            return new ErrorResponse(error, HttpStatusCode.BadRequest, null);
        }

        // public static ErrorResponse CreateErrorResponse(this CompositeValidationException ex)
        // {
        //     var errors = ex.ValidationResults.Select(v => v.ErrorMessage).ToList();
        //     var error = string.Join(";", errors);
        //
        //     return new ErrorResponse(error, HttpStatusCode.BadRequest);
        // }
        //
        // public static ErrorResponse CreateErrorResponse(this ValidationException ex) =>
        //     new ErrorResponse(ex.Message, HttpStatusCode.BadRequest);
        //
        // public static ErrorResponse CreateErrorResponse(this AuthorizationException ex) =>
        //     new ErrorResponse(ex.Message, HttpStatusCode.Forbidden);
        
        public static ErrorResponse CreateErrorResponse(this Exception ex) =>
            new ErrorResponse(ex.Message, HttpStatusCode.InternalServerError, null);
    }
}