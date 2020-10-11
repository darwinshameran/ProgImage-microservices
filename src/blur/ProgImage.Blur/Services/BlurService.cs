using ImageMagick;
using ProgImage.Blur.Domain.Services;

namespace ProgImage.Blur.Services
{
    public class BlurService : IBlurService
    {
        public byte[] BlurImage(byte[] image, double radius, double sigma)
        {
            MagickReadSettings readSettings = new MagickReadSettings();
            
            using MagickImage blurredImage = new MagickImage(image, readSettings);
            blurredImage.Blur(radius, sigma);
            
            return blurredImage.ToByteArray();
        }
    }
}