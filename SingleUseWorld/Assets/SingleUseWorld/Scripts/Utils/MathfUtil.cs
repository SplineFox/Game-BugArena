using SingleUseWorld;
using UnityEngine;

public static class MathfUtil
{
    public static float Remap(float value, RangeFloat fromRange, RangeFloat toRange)
    {
        float t = fromRange.GetInverseLerpedValue(value);
        float remapedValue = toRange.GetLerpedValue(t);
        return remapedValue;
    }

    public static float Remap(float value, float inputFrom, float inputTo, float outputFrom, float outputTo)
    {
        float t = Mathf.InverseLerp(inputFrom, inputTo, value);
        float remapedValue = Mathf.Lerp(outputFrom, outputTo, t);
        return remapedValue;
    }
}