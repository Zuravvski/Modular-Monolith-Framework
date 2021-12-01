using System;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Zuravvski.Infrastructure.Exceptions
{
    public sealed class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IExceptionMapperRegistry _exceptionMapperRegistry;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(IExceptionMapperRegistry exceptionMapperRegistry, ILogger<ErrorHandlerMiddleware> logger)
        {
            _exceptionMapperRegistry = exceptionMapperRegistry;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var exceptionResponse = _exceptionMapperRegistry.Resolve(ex)?.Map(ex);
            context.Response.StatusCode = (int)(exceptionResponse?.StatusCode ?? HttpStatusCode.BadRequest);
            var response = exceptionResponse?.Response;

            if (response is null)
            {
                await context.Response.WriteAsync(string.Empty);
                return;
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;
            await JsonSerializer.SerializeAsync(context.Response.Body, exceptionResponse);
        }
    }
}
