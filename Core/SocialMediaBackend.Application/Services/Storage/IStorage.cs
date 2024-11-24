using Microsoft.AspNetCore.Http;

namespace SocialMediaBackend.Application.Services.Storage
{
    public interface IStorage
    {
        List<string> GetFiles(string pathOrContainerName);
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);
        Task DeleteAsync(string pathOrContainerName, string fileName);
        bool HasFile(string pathOrContainerName, string fileName);
    }
}
