using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Storage.Domain.Services;

namespace ProgImage.Storage.Services
{
    public class StorageService : IStorageService
    {
        public async Task<byte[]> OpenFile(string filePath)
        {
            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task SaveFile(IFormFile file, string filePath, FileMode fileMode)
        {
            await using FileStream stream = new FileStream(filePath, fileMode);

           await file.CopyToAsync(stream);
        }
    }
}