using System;
using ProgImage.Transformation.Domain.Events;

// ReSharper disable All

namespace ProgImage.Resize.Events
{
    public class TransformationCompressStartEvent : BaseEvent
    {
        public int Quality { get; set; }
    }
}
