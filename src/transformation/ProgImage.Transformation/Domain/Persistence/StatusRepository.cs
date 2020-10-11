using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgImage.Transformation.Domain.Models;
using ProgImage.Transformation.Domain.Persistence.Database;
using ProgImage.Transformation.Domain.Repositories;

namespace ProgImage.Transformation.Domain.Persistence
{
    public class StatusRepository : BaseRepository, IStatusRepository
    {
        public StatusRepository(AppDbContext context) : base(context)
        {
        }
        
        public async Task AddAsync(TransformationStatus transformationStatus)
        {
            await Context.Status
                .AddAsync(transformationStatus);
            await Context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(TransformationStatus transformationStatus)
        {
            Context.Update(transformationStatus);
            await Context.SaveChangesAsync();
        }
        
        public async Task<TransformationStatus> FindByStatusIdAsync(Guid statusId)
        {
            return await Context.Status
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.StatusId == statusId);
        }
    }
}