using Core.Entities.Exceptions;
using Core.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";

            string message = "";
            if (e is ValidationException validationError)
            {
                message = "Validation Error";
                var validationList = validationError.Errors.Select(x => x.ErrorMessage).ToList();

                //liste boşsa business içerisinden atılan validation exception message ı aldık.
                if (validationList.Count == 0)
                {
                    validationList.Add(e.Message);
                }

                httpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    Success = false,
                    StatusCode = StatusCodes.Status406NotAcceptable,
                    Message = message,
                    Data = validationList
                }.ToString());
            }
            else if (e is NotFoundException notFoundError)
            {
                message = "Data Not Found";

                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = message,
                    Data = notFoundError.Id
                }.ToString());
            }

            message = "Internal Server Error";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                Success = false,
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}