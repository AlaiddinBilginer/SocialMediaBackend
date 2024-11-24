namespace SocialMediaBackend.Application.Services.Storage
{
    public interface IStorageService : IStorage
    {
        public string StorageName { get; }
    }
}
