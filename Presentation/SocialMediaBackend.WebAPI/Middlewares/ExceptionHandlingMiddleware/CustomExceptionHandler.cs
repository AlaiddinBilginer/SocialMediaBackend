using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;


namespace SocialMediaBackend.WebAPI.Middlewares.ExceptionHandlingMiddleware
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            var statusCode = StatusCodes.Status500InternalServerError;
            var errorResponse = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = "Sunucu hatası meydana geldi."
            };

            switch (exception)
            {
                case FluentValidation.ValidationException validationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    errorResponse.StatusCode = statusCode;
                    errorResponse.Message = "Doğrulama hatası meydana geldi.";
                    errorResponse.Errors = validationException.Errors.Select(e => new ValidationError
                    {
                        PropertyName = e.PropertyName,
                        ErrorMessage = e.ErrorMessage
                    }).ToList();
                    break;
            }

            httpContext.Response.StatusCode = statusCode;

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);

            return true;
        }
    }
}

