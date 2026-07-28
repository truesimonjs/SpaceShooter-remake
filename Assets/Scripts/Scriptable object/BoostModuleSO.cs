using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Modules/BoostModule", order = 1)]
public class BoostModuleSO : ShipModuleScriptable
{
    public override Type Type => typeof (BoostModule);
    public float speed = 4;
    public float energyCost = 2;

}
