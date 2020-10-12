using System;

namespace ProgImage.Transformation.Domain.Events
{
    public class TransformationThumbnailStartEvent : BaseEvent
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
