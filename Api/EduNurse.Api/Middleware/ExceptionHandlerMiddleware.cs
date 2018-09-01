using System;
using System.Net;
using System.Threading.Tasks;
using EduNurse.Api.Shared;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EduNurse.Api.Middleware
{
    internal class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _request;

        public ExceptionHandlerMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                // TODO: Use logger here
                Console.WriteLine(ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex is UnauthorizedAccessException
                ? (int)HttpStatusCode.Unauthorized
                : (int)HttpStatusCode.InternalServerError;

            var result = Result.Failure(ex.Message);
            var responseBody = JsonConvert.SerializeObject(result);

            return context.Response.WriteAsync(responseBody);
        }
    }
}
