using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Http;

namespace ProgImage.Storage.Helpers
{
    public class Utils
    { 
        // Ref: https://stackoverflow.com/questions/55869/determine-file-type-of-an-image/12451102#12451102
        public ImageFormat GetImageFormat(byte[] image)
        {
            var bmp = Encoding.ASCII.GetBytes("BM"); // BMP
            var gif = Encoding.ASCII.GetBytes("GIF"); // GIF
            var png = new byte[] {137, 80, 78, 71}; // PNG
            var jpeg = new byte[] {255, 216, 255, 224}; // jpeg
            var jpeg2 = new byte[] {255, 216, 255, 225}; // jpeg canon

            var buffer = new byte[4];

            if (bmp.SequenceEqual(image.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(image.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(image.Take(png.Length)))
                return ImageFormat.png;

            if (jpeg.SequenceEqual(image.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(buffer.Take(jpeg2.Length)))
            {
                return ImageFormat.jpeg;
            } 
            
            return ImageFormat.unknown;
        }

        public async Task<string> GetImageExtensionAsync(IFormFile image)
        {
            var fileExtension = Path.GetExtension(image.FileName);

            if (!string.IsNullOrEmpty(fileExtension))
            {
                return fileExtension;
            }
            
            await using MemoryStream memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream); 
            byte[] imageBytes = memoryStream.ToArray();

            fileExtension = GetImageFormat(imageBytes).ToString();

            return $".{fileExtension}";
        } 
        
        public byte[] ConvertImage(byte[] image, string format)
        {
            MagickImage newImage = new MagickImage(image)
            {
                Format = (MagickFormat) Enum.Parse(typeof(MagickFormat), format.ToTitleCase())
            };

            return newImage.ToByteArray();
        }
        
        public string GenerateFileName(string filename, string extension)
        {
            string fileName;

            do
            {
                fileName = $"{Path.GetTempPath()}{filename}{extension}";
            } while (File.Exists(fileName));

            return fileName;
        }
    }
}