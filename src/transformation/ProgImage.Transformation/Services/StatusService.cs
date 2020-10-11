using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Models;
using ProgImage.Transformation.Domain.Repositories;
using ProgImage.Transformation.Domain.Services;

namespace ProgImage.Transformation.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;


        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        
        public async Task<TransformationStatusResponse> AddStatusAsync(TransformationStatus transformationStatus)
        {
            try
            {
                await _statusRepository.AddAsync(transformationStatus);
            }
            catch (DbUpdateException)
            {
                return new TransformationStatusResponse(false, "Error: Something went whilst creating the status.", transformationStatus);
            }        
            
            return new TransformationStatusResponse(true, null, transformationStatus);
        }
        
        
        public async Task<TransformationStatusResponse> UpdateStatusAsync(TransformationStatus transformationStatus)
        {
            try
            {
                await _statusRepository.UpdateAsync(transformationStatus);
            }
            catch (DbUpdateException)
            {
                return new TransformationStatusResponse(false, "Error: Something went whilst updating the status.", transformationStatus);
            }        
            
            return new TransformationStatusResponse(true, null, transformationStatus);
        }


        public async Task<TransformationStatusResponse> FindByStatusIdAsync(Guid statusId)
        {
            TransformationStatus status = await _statusRepository.FindByStatusIdAsync(statusId);
            
            return new TransformationStatusResponse(true, null, status);
                
        }

    }
}