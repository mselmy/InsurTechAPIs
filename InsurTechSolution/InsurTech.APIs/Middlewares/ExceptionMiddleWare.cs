using InsurTech.APIs.Errors;
using System.Net;
using System.Text.Json;

namespace InsurTech.APIs.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next , ILogger<ExceptionMiddleWare> logger , IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                  await _next.Invoke(context);
            }
            catch(Exception e) 
            {
                _logger.LogError(e ,e.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                var Response = _env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace.ToString()) : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError) ;

                var option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonResponse = JsonSerializer.Serialize(Response,option);

                context.Response.WriteAsync(JsonResponse);

            }
        }
    }
}
