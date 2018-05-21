using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static Vector3 ClampToWorld(Vector3 v)
    {
        v.x = Mathf.Clamp(v.x, -2.75f, 2.75f);
        v.y = Mathf.Clamp(v.y, -4.5f, 4.5f);
        return v;
    }
}
