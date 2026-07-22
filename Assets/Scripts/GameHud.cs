using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
    [SerializeField] private GameObject player;
    //energy
    private ShipCore core;
    [SerializeField] private Image energyImage;
    //health
    private Entity entity;
    [SerializeField] private Image healthImage;
    //
    //private ShipShieldModule shieldModule;
    [SerializeField]private Image shieldImage;
    private void Start()
    {
        //energy

        core = player.GetComponentInChildren<ShipCore>();
        core.OnEnergyChange += Core_OnEnergyChange; ; ;
        energyImage.fillAmount = core.energy / core.MaxEnergy;
        //health
        entity = player.GetComponentInChildren<Entity>();
        entity.OnTakeDamage += Entity_OnTakeDamage;
        //
        core.OnShieldChange += Core_OnShieldChange;
        shieldImage.fillAmount = 0;
        
    }

    private void Core_OnEnergyChange(object sender, System.EventArgs e)
    {
        energyImage.fillAmount = core.energy / core.MaxEnergy; 
    }

    private void Core_OnShieldChange(object sender, System.EventArgs e)
    {
        shieldImage.fillAmount = core.Shields / core.MaxShields;
    }

    private void Entity_OnTakeDamage(object sender, System.EventArgs e)
    {
        healthImage.fillAmount = entity.Health / entity.MaxHealth;
    }

 

}
