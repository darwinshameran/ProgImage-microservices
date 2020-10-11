using System;
// ReSharper disable All

namespace ProgImage.Resize.Events
{
    public class TransformationRotateStartEvent
    {
        public Guid StatusId { get; set; }
        public string Url { get; set; }
        public int Degrees { get; set; }
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
