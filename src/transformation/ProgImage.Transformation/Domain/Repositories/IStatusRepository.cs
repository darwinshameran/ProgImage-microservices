using System;
using System.Threading.Tasks;
using ProgImage.Transformation.Domain.Models;

namespace ProgImage.Transformation.Domain.Repositories
{
    public interface IStatusRepository
    {
        Task AddAsync(TransformationStatus transformationStatus);
        Task UpdateAsync(TransformationStatus transformationStatus);

        Task<TransformationStatus> FindByStatusIdAsync(Guid statusId);
    }
}