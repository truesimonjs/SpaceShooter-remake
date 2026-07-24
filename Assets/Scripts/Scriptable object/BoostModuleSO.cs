using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Modules/BoostModule", order = 1)]
public class BoostModuleSO : ShipModuleScriptable
{
    public override Type Type => typeof (BoostModule);
    public float speed = 4;
    public float rotationSpeed = 10;
    public float duration = 2f;
    public float cooldown = 3f;

}
