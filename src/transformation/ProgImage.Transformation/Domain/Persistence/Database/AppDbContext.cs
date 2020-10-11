using Microsoft.EntityFrameworkCore;
using ProgImage.Transformation.Domain.Models;

namespace ProgImage.Transformation.Domain.Persistence.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TransformationStatus> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Set `ImageId` to primary key.
            builder.Entity<TransformationStatus>()
                .HasKey(i => i.StatusId);
        }
    }
}