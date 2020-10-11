using ImageMagick;
using ProgImage.Resize.Domain.Services;

namespace ProgImage.Resize.Services
{
    public class Thumbnail : IThumbnail
    {
        public byte[] Resize(byte[] image, int width, int height)
        {
            MagickReadSettings readSettings = new MagickReadSettings();

            using MagickImage resizedImage = new MagickImage(image, readSettings);
            MagickGeometry size = new MagickGeometry(width, height)
            {
                IgnoreAspectRatio = true
            };

            resizedImage.Resize(size);

            return resizedImage.ToByteArray();
        }
    }
}