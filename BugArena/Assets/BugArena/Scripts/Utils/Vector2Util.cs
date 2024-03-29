﻿using UnityEngine;

public static class Vector2Util
{
    public static Vector2 Abs(Vector2 vector)
    {
        return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }
}
