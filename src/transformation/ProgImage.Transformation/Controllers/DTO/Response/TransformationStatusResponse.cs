using ProgImage.Transformation.Domain.Models;

namespace ProgImage.Transformation.Controllers.DTO.Response
{
    public class TransformationStatusResponse : BaseResponse
    {
        public TransformationStatus TransformationStatus { get; }
        
        public TransformationStatusResponse(bool success, string message, TransformationStatus transformationStatus) : base(success, message)
        {
            TransformationStatus = transformationStatus;
        }
    }
}