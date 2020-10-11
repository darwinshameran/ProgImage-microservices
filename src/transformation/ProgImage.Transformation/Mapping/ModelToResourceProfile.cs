using AutoMapper;
using ProgImage.Transformation.Domain.Events;
using ProgImage.Transformation.Domain.Models;

namespace ProgImage.Transformation.Mapping
{
    public class ModelToDto : Profile
    {
        public ModelToDto()
        {
            CreateMap<TransformationStatusEvent, TransformationStatus>()
                .ForMember(
                    a => a.Status, opt => opt.MapFrom(a => a.Status));        }
    }
}