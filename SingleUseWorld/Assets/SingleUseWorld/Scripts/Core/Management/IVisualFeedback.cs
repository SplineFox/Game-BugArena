﻿namespace SingleUseWorld
{
    public interface IVisualFeedback
    {
        IHitTimer Timer { get; set; }
        CameraShaker Shaker { get; set; }
    }
}