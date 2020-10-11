using System;

namespace ProgImage.Storage.Controllers.DTO
{
    public class ImageDTO
    {
        public Guid ImageId { get; set; }
        public string ImageFilePath { get; set; }
        public long CreatedAt { get; set; }
    }
}