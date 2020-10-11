using ImageMagick;
using ProgImage.Rotate.Domain.Services;

namespace ProgImage.Rotate.Services
{
    public class RotateService : IRotateService
    {
        public byte[] RotateImage(byte[] image, int degrees)
        {
            MagickReadSettings readSettings = new MagickReadSettings();
            using MagickImage blurredImage = new MagickImage(image, readSettings);
            blurredImage.Rotate(degrees);
            
            return blurredImage.ToByteArray();
        }
    }
}