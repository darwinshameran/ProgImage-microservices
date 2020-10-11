using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProgImage.Storage.Domain.Models;

namespace ProgImage.Storage.Domain.Persistence.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Set `ImageId` to primary key.
            builder.Entity<Image>()
                .HasKey(i => i.ImageId);
        }
    }
}