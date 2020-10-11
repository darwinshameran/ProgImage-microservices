using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProgImage.Storage.Domain.Services
{
    public interface IStorageService
    {
        Task<byte[]> OpenFile(string filePath);
        Task SaveFile(IFormFile file, string filePath, FileMode fileMode);
    }
}