namespace Shared.Interfaces
{
    public interface IFileService
    {
        Task<string> ReadFileAsync(string filePath);
        Task WriteFileAsync(string filePath, string content);
        bool FileExists(string filePath);
    }
}