using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace SocialMediaBackend.WebAPI.Middlewares.ExceptionHandlingMiddleware
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            var (statusCode, errorMessage) = exception switch
            {
                _ => (StatusCodes.Status500InternalServerError, "Sunucu hatası meydana geldi.")
            };

            await httpContext.Response.WriteAsync(new ErrorDetail
            {
                StatusCode = statusCode,
                Message = errorMessage,
            }.ToString(), cancellationToken);

            return true;
        }
    }
}
