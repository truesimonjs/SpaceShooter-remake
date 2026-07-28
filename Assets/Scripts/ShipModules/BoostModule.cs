using UnityEngine;
using System.Collections;

public class BoostModule : ShipModule<BoostModuleSO>
{

    [field: SerializeField] public string Activator { get; set; } = "Boost";

    ShipController ship;
    private float oldSpeed;
    public override void Start()
    {
        base.Start();
        ship = GetComponentInParent<ShipController>();
        oldSpeed = ship.shipSpeed;
        master.abillityModules.Add(Activator, this);

    }
    private bool boosting;
    private bool Boosting { get => boosting; 
        set
        {
            if (value != boosting)
            {
                boosting = value;
                ship.upSpeed = boosting ? Stats.speed : oldSpeed;
            }
        } 
    }
    private void Update()
    {
        Boosting = Boosting && core.ChangeEnergyBool(-Stats.energyCost*Time.deltaTime);
      
    }

  
    public override void UseAbillity()
    {
        if (Boosting) Boosting = false; 
        else if (core.CheckEnergy(-Stats.energyCost)) Boosting = true;
    }
}
