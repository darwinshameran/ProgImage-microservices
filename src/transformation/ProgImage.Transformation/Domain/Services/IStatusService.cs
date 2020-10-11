using System;
using System.Threading.Tasks;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Models;

namespace ProgImage.Transformation.Domain.Services
{
    public interface IStatusService
    {
        Task<TransformationStatusResponse> AddStatusAsync(TransformationStatus transformationStatus);
        Task<TransformationStatusResponse> UpdateStatusAsync(TransformationStatus transformationStatus);
        Task<TransformationStatusResponse> FindByStatusIdAsync(Guid statusId);

    }
}