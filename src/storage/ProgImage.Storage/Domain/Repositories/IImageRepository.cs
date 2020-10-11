using System;
using System.Threading.Tasks;
using ProgImage.Storage.Domain.Models;

namespace ProgImage.Storage.Domain.Repositories
{
    public interface IImageRepository
    {
        Task AddAsync(Image image);
        Task<Image> FindByImageIdAsync(Guid imageId);
    }
}