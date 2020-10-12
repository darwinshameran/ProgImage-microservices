using System;

namespace ProgImage.Transformation.Domain.Events
{
    public class BaseEvent
    {
        public Guid StatusId { get; set; }
        public string Url { get; set; }
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}