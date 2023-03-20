using UnityEngine;

namespace SingleUseWorld
{
    public class VisualFeedback : IVisualFeedback
    {
        public IHitTimer Timer { get; set; }
        public CameraShaker Shaker { get; set; }
    }
}