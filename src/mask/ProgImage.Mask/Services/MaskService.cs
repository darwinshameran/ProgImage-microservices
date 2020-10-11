using ImageMagick;
using ProgImage.Mask.Domain.Services;

namespace ProgImage.Mask.Services
{
    public class MaskService : IMaskService
    {
        public MaskService()
        {
        }

        public byte[] MaskImage(byte[] image)
        {
            MagickReadSettings readSettings = new MagickReadSettings();
            using MagickImage maskedImage = new MagickImage(image, readSettings);
            
            maskedImage.Alpha(AlphaOption.Set);
            maskedImage.ColorFuzz = new Percentage(50);
            maskedImage.Settings.FillColor = MagickColors.None;
            maskedImage.FloodFill(MagickColors.White, 0, 0);

            return maskedImage.ToByteArray();
        }
    }
}