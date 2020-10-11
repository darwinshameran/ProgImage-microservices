using ProgImage.Storage.Domain.Models;

#nullable enable
namespace ProgImage.Storage.Domain.Services.RO
{
    public class ImageFindResponse : BaseResponse
    {
        public ImageFindResponse(bool success, string message, Image? image) : base(success, message)
        {
            Image = image;
        }

        public Image? Image { get; }
    }
}