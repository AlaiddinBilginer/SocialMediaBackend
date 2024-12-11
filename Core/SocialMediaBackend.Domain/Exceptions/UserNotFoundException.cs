namespace SocialMediaBackend.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Kullanıcı bulunamadı.")
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}
