using System;

namespace ProgImage.Transformation.Domain.Models
{
    public class TransformationStatus
    {
        public Guid StatusId { get; set; }
        public Guid? ImageId { get; set; }
        public string Status { get; set; }
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}