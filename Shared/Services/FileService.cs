using Shared.Interfaces;

namespace Shared.Services
{
    public class FileService : IFileService
    {
        public async Task<string> ReadFileAsync(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }

        public async Task WriteFileAsync(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}

