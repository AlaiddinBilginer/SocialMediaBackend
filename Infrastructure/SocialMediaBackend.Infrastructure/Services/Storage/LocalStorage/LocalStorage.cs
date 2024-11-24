using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SocialMediaBackend.Application.Services.Storage.LocalStorage;

namespace SocialMediaBackend.Infrastructure.Services.Storage.LocalStorage
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles().Select(f => f.Name).ToList();
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> datas = new();

            foreach (IFormFile file in files)
            {
                string fileNewName = FileRenameAsync(uploadPath, file.FileName);
                string fullPath = Path.Combine(uploadPath, fileNewName);
                await CopyFileAsync(fullPath, file);

                datas.Add((fileNewName, $"{path}\\{fileNewName}"));
            }

            return datas;
        }

        public async Task DeleteAsync(string path, string fileName)
        {
            File.Delete($"{path}\\{fileName}");
        }

        public bool HasFile(string path, string fileName)
        {
            return File.Exists($"{path}\\{fileName}");
        }

        private async Task CopyFileAsync(string fullPath, IFormFile file)
        {
            using FileStream fileStream = new FileStream
                (fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
    }
}
