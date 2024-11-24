using Microsoft.AspNetCore.Http;
using SocialMediaBackend.Application.Services.Storage;

namespace SocialMediaBackend.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public List<string> GetFiles(string pathOrContainerName)
            => _storage.GetFiles(pathOrContainerName);

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
            => await _storage.UploadAsync(pathOrContainerName, files);

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
            => await _storage.DeleteAsync(pathOrContainerName, fileName);

        public bool HasFile(string pathOrContainerName, string fileName)
            => _storage.HasFile(pathOrContainerName, fileName);
    }
}
