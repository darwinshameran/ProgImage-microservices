using ImageMagick;
using ProgImage.Compress.Domain.Services;

namespace ProgImage.Compress.Services
{
    public class CompressService : ICompressService
    {
        public byte[] CompressImage(byte[] image, int quality)
        {
            MagickReadSettings readSettings = new MagickReadSettings();

            using MagickImage compressedImage = new MagickImage(image, readSettings)
            {
                Quality = quality
            };
            
            return compressedImage.ToByteArray();
        }
    }
}