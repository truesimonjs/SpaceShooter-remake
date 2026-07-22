using UnityEngine;
using System;
using UnityEngine.UI;
public abstract class ShipModuleScriptable : ScriptableObject
{
    public abstract Type Type { get; }
    public Sprite icon;
    public virtual bool IsUnique { get => false; }
}

   
