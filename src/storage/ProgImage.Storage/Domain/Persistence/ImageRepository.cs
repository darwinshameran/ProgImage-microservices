using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgImage.Storage.Domain.Models;
using ProgImage.Storage.Domain.Persistence.Database;
using ProgImage.Storage.Domain.Repositories;

namespace ProgImage.Storage.Domain.Persistence
{
    public class ImageRepository : BaseRepository, IImageRepository
    {
        public ImageRepository(AppDbContext context) : base(context)
        {
        }
        
        public async Task AddAsync(Image image)
        {
            await _context.Images
                .AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task<Image> FindByImageIdAsync(Guid imageId)
        {
            return await _context.Images
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.ImageId == imageId);
        }
    }
}