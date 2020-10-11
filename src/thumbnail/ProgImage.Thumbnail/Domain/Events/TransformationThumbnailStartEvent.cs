using System;
// ReSharper disable All

namespace ProgImage.Resize.Events
{
    public class TransformationThumbnailStartEvent
    {
        public Guid StatusId { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
