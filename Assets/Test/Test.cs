using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
}


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TextAttribute : Attribute
{
    public bool canSwitch;

    public TextAttribute(bool canSwitch)
    {
        this.canSwitch = canSwitch;
    }
}