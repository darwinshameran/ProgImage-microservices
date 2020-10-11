using System;

namespace ProgImage.Blur.Models.DTO
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string ImageFilePath { get; set; }
        public long CreatedAt { get; set; }
    }
}