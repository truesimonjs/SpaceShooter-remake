using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Modules/Storage", order = 1)]
public class StorageSO : ShipModuleScriptable
{
    public override Type Type => typeof (StorageModule);
    public int Capacity = 5;
   
}
