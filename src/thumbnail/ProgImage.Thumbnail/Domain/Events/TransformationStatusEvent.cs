using System;

namespace ProgImage.Resize.Domain.Events
{
    public class TransformationStatusEvent
    { 
        public Guid StatusId { get; set; }
        public Guid? ImageId { get; set; }
        public string Status { get; set; }
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}