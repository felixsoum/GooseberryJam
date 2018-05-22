using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public const float XMax = 2.75f;
    public const float YMax = 3.5f;

    public static Vector3 ClampToWorld(Vector3 v)
    {
        v.x = Mathf.Clamp(v.x, -XMax, XMax);
        v.y = Mathf.Clamp(v.y, -YMax, YMax);
        return v;
    }
}
