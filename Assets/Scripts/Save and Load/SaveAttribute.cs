using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Field)]
public class SaveAttribute : System.Attribute
{
    public SaveAttribute() { }

    public FieldInfo info;
}
