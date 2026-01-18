using AIGenerateStack.Server.Model;
using System.Net;
using System.Text.Json;

namespace AIGenerateStack.Server.Middleware
{
  
    
        public sealed class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly IWebHostEnvironment _env;
            private readonly ILogger<ExceptionMiddleware> _logger;

            public ExceptionMiddleware(
                RequestDelegate next,
                IWebHostEnvironment env,
                ILogger<ExceptionMiddleware> logger)
            {
                _next = next;
                _env = env;
                _logger = logger;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception");

                    await HandleExceptionAsync(context, ex);
                }
            }

            private async Task HandleExceptionAsync(
                HttpContext context,
                Exception exception)
            {
                context.Response.ContentType = "application/json";

                var statusCode = exception switch
                {
                    ArgumentException => HttpStatusCode.BadRequest,
                    InvalidOperationException => HttpStatusCode.Conflict,
                    KeyNotFoundException => HttpStatusCode.NotFound,
                    _ => HttpStatusCode.InternalServerError
                };

                context.Response.StatusCode = (int)statusCode;

                var response = new ErrorResponse
                {
                    TraceId = context.TraceIdentifier,
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message,
                    Details = _env.IsDevelopment()
                        ? exception.StackTrace
                        : null
                };

                var json = JsonSerializer.Serialize(
                    response,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                await context.Response.WriteAsync(json);
            }
        }
    }


