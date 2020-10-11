using ProgImage.Storage.Domain.Persistence.Database;

namespace ProgImage.Storage.Domain.Persistence
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        /// <summary>
        ///     A base class from which every other repository will inherit from.
        /// </summary>
        /// <param name="context"></param>
        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}