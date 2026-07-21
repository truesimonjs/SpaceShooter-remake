using UnityEngine;
using System;
public abstract class ShipModuleScriptable : ScriptableObject
{
    public abstract Type Type { get; }
    public virtual bool IsUnique { get => false; }
}

   
