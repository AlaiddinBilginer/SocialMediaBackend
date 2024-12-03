namespace SocialMediaBackend.WebAPI.Middlewares.ExceptionHandlingMiddleware
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<ValidationError>? Errors { get; set; }
    }
}
