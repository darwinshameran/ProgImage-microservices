using System;
using ProgImage.Transformation.Domain.Events;

// ReSharper disable All

namespace ProgImage.Resize.Events
{
    public class TransformationBlurStartEvent : BaseEvent
    {
        public double Radius { get; set; }
        public double Sigma { get; set; }
    }
}
