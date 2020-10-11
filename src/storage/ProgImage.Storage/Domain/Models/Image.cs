using System;

namespace ProgImage.Storage.Domain.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string ImageFilePath { get; set; }
        public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}