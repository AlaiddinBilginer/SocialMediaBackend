using PersonelBlogBackend.Infrastructure.Operations;

namespace SocialMediaBackend.Infrastructure.Services.Storage
{
    public class Storage
    {
        public string FileRenameAsync(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string regulatedFileName = NameOperation.CharacterRegulatory(oldName);

            var files = Directory.GetFiles(path, regulatedFileName + "*");

            if (files.Length == 0)
                return $"{regulatedFileName}-1{extension}";

            List<int> fileNumbers = new List<int>();
            foreach (var file in files)
            {
                string fileWithoutPath = Path.GetFileNameWithoutExtension(file);
                int lastHyphenIndex = fileWithoutPath.LastIndexOf("-");

                if (lastHyphenIndex != -1 && int.TryParse(fileWithoutPath.Substring(lastHyphenIndex + 1), out int fileNumber))
                {
                    fileNumbers.Add(fileNumber);
                }
            }

            int nextNumber = (fileNumbers.Count > 0) ? fileNumbers.Max() + 1 : 1;

            return $"{regulatedFileName}-{nextNumber}{extension}";
        }
    }
}
