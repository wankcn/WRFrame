using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OtherEx
{
    public static void Log(this object obj)
    {
        Debug.Log(obj.ToString());
    }


    public static Vector3 GetForward(this Transform transform)
    {
        return transform.forward;
    }

    public static void ResetLocal(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}