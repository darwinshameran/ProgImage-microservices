using System;
// ReSharper disable All

namespace ProgImage.Resize.Events
{
    public class TransformationBlurStartEvent
    {
        public Guid StatusId { get; set; }
        public string Url { get; set; }
        public double Radius { get; set; }
        public double Sigma { get; set; }
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
