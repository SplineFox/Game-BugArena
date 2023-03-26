using System;
using UnityEngine;

namespace SingleUseWorld
{
    [Serializable]
    public struct RangeFloat
    {
        public float start;
        public float end;

        public RangeFloat(float start, float end)
        {
            this.start = start;
            this.end = end;
        }

        public float GetRandomValue()
        {
            return UnityEngine.Random.Range(start, end);
        }

        public float GetLerpedValue(float t)
        {
            return Mathf.Lerp(start, end, t);
        }

        public float GetInverseLerpedValue(float value)
        {
            return Mathf.InverseLerp(start, end, value);
        }
    }
}
