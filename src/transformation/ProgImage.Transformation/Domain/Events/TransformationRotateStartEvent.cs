using System;
using ProgImage.Transformation.Domain.Events;

// ReSharper disable All

namespace ProgImage.Resize.Events
{
    public class TransformationRotateStartEvent : BaseEvent
    {
        public int Degrees { get; set; }
    }
}
