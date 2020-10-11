using AutoMapper;
using ProgImage.Storage.Controllers.DTO;
using ProgImage.Storage.Domain.Models;

namespace ProgImage.Storage.Mapping
{
    public class ModelToDto : Profile
    {
        public ModelToDto()
        {
            CreateMap<Image, ImageDTO>()
                .ForMember(a => a.ImageId, opt => opt.MapFrom(a => a.ImageId));
        }
    }
}