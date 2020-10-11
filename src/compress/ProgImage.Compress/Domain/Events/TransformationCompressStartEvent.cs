using System;
// ReSharper disable All

namespace ProgImage.Resize.Events
{
    public class TransformationCompressStartEvent
    {
        public Guid StatusId { get; set; }
        public string Url { get; set; }
        public int Quality { get; set; }
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
