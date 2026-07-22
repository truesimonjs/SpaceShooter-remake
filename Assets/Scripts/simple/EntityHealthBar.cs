using UnityEngine;
using UnityEngine.UI;
using System;

public class EntityHealthBar : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private Slider slider;
    //[SerializeField] private ShipShieldModule shipShield;
    [SerializeField] private ShipCore core;
    [SerializeField] private Image shieldImage;

    private void Start()
    {
     
        if ( entity == null) entity = GetComponentInParent<Entity>(true);
        if (slider == null) slider = entity.GetComponentInChildren<Slider>(true);
        if (core == null) core = entity.GetComponentInChildren<ShipCore>(true);
        slider.maxValue = entity.MaxHealth;
        slider.value = entity.Health;


        entity.HealthChange += Entity_HealthChange;

        core.OnShieldChange += Core_OnShieldChange;
        Core_OnShieldChange(this, EventArgs.Empty);


    }

    private void Core_OnShieldChange(object sender, System.EventArgs e)
    {
        shieldImage.fillAmount = core.MaxShields == 0? 0:core.Shields / core.MaxShields;
        updateBar();
    }



    private void Entity_HealthChange(object sender, System.EventArgs e)
    {
        
       
        slider.value = entity.Health;
        updateBar();
    }
    private void updateBar()
    {
     
            slider.gameObject.SetActive(entity.Health < entity.MaxHealth ||core.Shields<core.MaxShields);
        
    }
    private void LateUpdate()
    {
        slider.transform.parent.rotation = Quaternion.identity;
    }
}
