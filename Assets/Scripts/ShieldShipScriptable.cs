using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Modules/ShieldModule", order = 1)]
public class ShieldShipScriptable : ScriptableObject
{
    public float maxShields = 10;
    public float shieldsPerSec = 1;
    public float CostPerShield = 1;
}
