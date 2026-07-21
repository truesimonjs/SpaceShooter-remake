using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
    [SerializeField] private GameObject player;
    //energy
    private ShipModuleMaster moduleMaster;
    [SerializeField] private Image energyImage;
    //health
    private Entity entity;
    [SerializeField] private Image healthImage;
    //
    private ShipShieldModule shieldModule;
    [SerializeField]private Image shieldImage;
    private void Start()
    {
        //energy

        moduleMaster = player.GetComponentInChildren<ShipModuleMaster>();
        moduleMaster.OnEnergyChange += ModuleMaster_OnEnergyChange; ;
        energyImage.fillAmount = moduleMaster.energy / moduleMaster.MaxEnergy;
        //health
        entity = player.GetComponentInChildren<Entity>();
        entity.OnTakeDamage += Entity_OnTakeDamage;
        //
        moduleMaster.OnEquipShield += ModuleMaster_OnEquipShield;
        shieldImage.fillAmount = 0;
    }

    private void ModuleMaster_OnEquipShield(object sender, ShipShieldModule e)
    {
        shieldModule = e;
        shieldModule.OnShieldChange += ShieldModule_OnShieldChange;
        shieldImage.fillAmount = shieldModule.shields / shieldModule.Stats.maxShields;
    }

    private void ShieldModule_OnShieldChange(object sender, System.EventArgs e)
    {
        shieldImage.fillAmount = shieldModule.shields / shieldModule.Stats.maxShields;
    }

    private void Entity_OnTakeDamage(object sender, System.EventArgs e)
    {
        healthImage.fillAmount = entity.Health / entity.MaxHealth;
    }

    private void ModuleMaster_OnEnergyChange(object sender, System.EventArgs e)
    {
        energyImage.fillAmount = moduleMaster.energy / moduleMaster.MaxEnergy;
    }

}
