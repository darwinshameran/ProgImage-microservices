namespace ProgImage.Storage.Domain.Services.RO
{
    public class ImageResponse : BaseResponse
    {
        public ImageResponse(bool success, string message, byte[] image) : base(success, message)
        {
            Image = image;
        }

        public byte[] Image { get; }
    }
}