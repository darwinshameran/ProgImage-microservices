using ProgImage.Storage.Domain.Models;

namespace ProgImage.Storage.Domain.Services.RO
{
    public class ImageUploadResponse : BaseResponse
    {
        public ImageUploadResponse(bool success, string message, Image image) : base(success, message)
        {
            Image = image;
        }

        public Image Image { get; }
    }
}