using Aplicacion.Common;
using System.Text.Json;

namespace WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = 500;
            string message = "Error interno del servidor";
            string type = "INTERNAL_ERROR";

            if (ex.Message.StartsWith("ERROR_DE_VALIDACION"))
            {
                statusCode = 400;
                message = ex.Message.Replace("ERROR_DE_VALIDACION", "").Trim();
                type = "VALIDATION_ERROR";
            }

            var result = JsonSerializer.Serialize(new
            {
                errorCode = statusCode,
                errorMessage= message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
