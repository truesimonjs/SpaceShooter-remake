using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShipEntity : Entity
{
    public List<IDamageAbleModule> damageAbleModules;
    
    
    
    
    public override void Start()
    {
        base.Start();
        damageAbleModules = GetComponentsInChildren<IDamageAbleModule>().ToList();
        Debug.Log(GetComponentsInChildren<IDamageAbleModule>().ToList().Count);
        damageAbleModules.Sort((x, y) => y.Priority - x.Priority );
        
    }
    public override void TakeDamage(float damage = 1)
    {


        foreach (var module in damageAbleModules)
        {


            damage = module.TryTakeDamage(damage);

            if (damage <= 0) break;
        }

        base.TakeDamage(damage);
    }
}
