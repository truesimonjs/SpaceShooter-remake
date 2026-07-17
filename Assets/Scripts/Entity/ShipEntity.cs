using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipEntity : Entity
{
    public List<DamageAbleModule> damageAbleModules;
    
    public override void TakeDamage(float damage = 1)
    {
  

        foreach (var module in damageAbleModules) 
        {
            
          
            damage = module.TryTakeDamage(damage);
            
            if (damage <= 0) break;
        }
         
        base.TakeDamage(damage);
    }
    public override void Start()
    {
        base.Start();
        damageAbleModules = GetComponentsInChildren<DamageAbleModule>().ToList();
        damageAbleModules.Sort((x, y) => y.priority - x.priority );
        
    }
}
