using Shared.Interfaces;

namespace Shared.Services
{
    public class FileService : IFileService
    {
        public async Task<string> ReadFileAsync(string filePath)
        {
            // Läser fil innehåll async (async = utan att pausa).
            return await File.ReadAllTextAsync(filePath);
        }

        // Metod som ska skriva texten till filen utan att pausa pga async.
        public async Task WriteFileAsync(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }

        // Kollar om filen existerar.
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
