using System;

namespace ProgImage.Rotate.Domain.Events
{
    public class TransformationStatusEvent
    { 
        public Guid StatusId { get; set; }
        public Guid? ImageId { get; set; }
        public string Status { get; set; }
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}