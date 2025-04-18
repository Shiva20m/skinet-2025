using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middlewares
{
    public class ExceptionMiddleware(IHostEnvironment env, RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex, IHostEnvironment env)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response  = env.IsDevelopment()
                ?new ApiErrorResponse(httpContext.Response.StatusCode, ex.Message, ex.StackTrace)
                : new ApiErrorResponse(httpContext.Response.StatusCode, ex.Message, "Internal Server Error");

            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            var json = JsonSerializer.Serialize(response, options);
            return httpContext.Response.WriteAsync(json);
        }
    }
    
}