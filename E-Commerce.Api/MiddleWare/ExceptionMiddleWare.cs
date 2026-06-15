using E_Commerce.Api.Errors;
using System.Net;
using System.Text.Json;

namespace E_Commerce.Api.MiddleWare
{
    public class ExceptionMiddleWare(IHostEnvironment env, RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptioAsync(context, ex, env);
            }
        }

        private static Task HandleExceptioAsync(HttpContext context, Exception ex, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment() ? new ApiErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : new ApiErrorResponse(context.Response.StatusCode, ex.Message, "Internal Server Error");

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            return context.Response.WriteAsync(json);
        }
    }
}
