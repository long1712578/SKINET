using SKINET.Errors;
using System.Net;
using System.Text.Json;

namespace SKINET.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception err)
            {
                _logger.LogError(err, err.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var reponse = _environment.IsDevelopment() ? new ApiException((int)HttpStatusCode.InternalServerError, err.Message, err.StackTrace.ToString())
                                                            : new ApiException((int)HttpStatusCode.InternalServerError);
                var json = JsonSerializer.Serialize(reponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
