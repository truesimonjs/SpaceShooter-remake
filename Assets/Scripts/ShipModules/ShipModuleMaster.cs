using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.EventSystems;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShipModuleMaster : MonoBehaviour
{




    //
    public event EventHandler OnModuleChange;
    private StorageModule storageModule;
    [SerializeField] public ShipModuleScriptable[] AllShipModuleSO = new ShipModuleScriptable[5];
    private ShipModule[] AllShipModuleComp = new ShipModule[5];
    private void Awake()
    {
        
        //this feels like a band aid but it should let ships understand the modules you put on before game start
        for (int i = 0; i < AllShipModuleSO.Length; i++)
        {
            if (AllShipModuleSO[i] != null) ActivateModule(i); ;
        }
    }

    public bool PickupModule(ShipModuleScriptable moduleSo)
    {
        if (storageModule == null) storageModule = GetComponent<StorageModule>();
        ShipModuleScriptable leftover = EquipModule(moduleSo);
        if (leftover != null)
        {
            if (storageModule == null) return false;
           return  storageModule.store(leftover);
        }
        return true;
    }
    public ShipModuleScriptable EquipModule(ShipModuleScriptable moduleSo,int? targetIndex = null)
    {
        

        if (targetIndex != null)
        {
            return EquipModuleOnSlot(moduleSo, targetIndex.Value);


        }
        for (int i = 0; i < AllShipModuleSO.Length; i++)
        {
           
            if (AllShipModuleSO[i] == null)
            {
                return EquipModuleOnSlot(moduleSo,i);
            }

        }
        return moduleSo;
    }
    private ShipModuleScriptable EquipModuleOnSlot(ShipModuleScriptable moduleSo, int targetIndex)
    {
        if (moduleSo.IsUnique && CheckForExistingModule(moduleSo)) return moduleSo;
        ShipModuleScriptable oldModuleSO = AllShipModuleSO[targetIndex];
        AllShipModuleSO[targetIndex] = moduleSo;
        //setup the component and delete the old one
        ActivateModule(targetIndex);
        OnModuleChange?.Invoke(this, EventArgs.Empty);
        
        return oldModuleSO;
    }
    private void ActivateModule(int index)
    {
        ShipModuleScriptable newModuleSO = AllShipModuleSO[index];

       if (AllShipModuleComp[index] != null) Destroy(AllShipModuleComp[index]);
        AllShipModuleComp[index] = (ShipModule)gameObject.AddComponent(newModuleSO.Type); ;
        AllShipModuleComp[index].GenericStats = newModuleSO;
    }
    private bool CheckForExistingModule(ShipModuleScriptable moduleSO)
    {
        foreach (ShipModule module in AllShipModuleComp)
        {
            if (moduleSO.Type == module?.GetType()) return true;
        }
        return false;
    }
}
