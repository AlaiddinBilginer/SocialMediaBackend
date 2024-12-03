namespace SocialMediaBackend.WebAPI.Middlewares.ExceptionHandlingMiddleware
{
    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
