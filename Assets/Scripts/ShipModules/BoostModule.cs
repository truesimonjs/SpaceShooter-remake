using UnityEngine;
using System.Collections;

public class BoostModule : ShipModule<BoostModuleSO>
{

    [field: SerializeField] public string Activator { get; set; } = "Boost";

    ShipController ship;
    private float nextActivate = 0f;
    private float oldSpeed;
    private float oldRotationSpeed;
    public override void Start()
    {
        base.Start();
        ship = GetComponentInParent<ShipController>();
        oldSpeed = ship.shipSpeed;
        oldRotationSpeed = ship.rotationSpeed;
        master.abillityModules.Add(Activator, this);

    }
    public override void UseAbillity()
    {
        StartCoroutine(Boost());
    }

    private IEnumerator Boost()
    {
        if (Time.time >= nextActivate)
        {
            ship.shipSpeed = Stats.speed;
            ship.rotationSpeed = Stats.rotationSpeed;
            yield return new WaitForSeconds(Stats.duration);
            ship.shipSpeed = oldSpeed;
            ship.rotationSpeed = oldRotationSpeed;
            nextActivate = Time.time + Stats.cooldown;
        }

    }
    private void OnDestroy()
    {
        
    }
}
