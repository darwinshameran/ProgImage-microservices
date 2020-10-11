using ProgImage.Transformation.Domain.Persistence.Database;

namespace ProgImage.Transformation.Domain.Persistence
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext Context;

        /// <summary>
        ///     A base class from which every other repository will inherit from.
        /// </summary>
        /// <param name="context"></param>
        protected BaseRepository(AppDbContext context)
        {
            Context = context;
        }
    }
}