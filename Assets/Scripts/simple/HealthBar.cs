using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private Slider slider;
    [SerializeField] private ShipShieldModule shipShield;
    [SerializeField] private Image shieldImage;

    private void Start()
    {
     
        if ( entity == null) entity = GetComponentInParent<Entity>(true);
        if (slider == null) slider = GetComponentInChildren<Slider>(true);
        slider.maxValue = entity.MaxHealth;
        slider.value = entity.Health;
        entity.HealthChange += Entity_HealthChange;

       if (shipShield== null) shieldImage.fillAmount = 0;

    }
    public void ShieldStart(ShipShieldModule shield)
    {
    
            shipShield = shield;
            shipShield.OnShieldChange += ShipShield_OnShieldChange;
            shieldImage.fillAmount = shipShield.shields / shipShield.Stats.maxShields;
        
      
    }
    private void ShipShield_OnShieldChange(object sender, System.EventArgs e)
    {
        shieldImage.fillAmount = shipShield.shields / shipShield.Stats.maxShields;
        updateBar();
    }

    private void Entity_HealthChange(object sender, System.EventArgs e)
    {
        
       
        slider.value = entity.Health;
        updateBar();
    }
    private void updateBar()
    {
        if ( shipShield == null)
        {
            slider.gameObject.SetActive(entity.Health < entity.MaxHealth);
        }
        else
        {
            slider.gameObject.SetActive(entity.Health < entity.MaxHealth ||shipShield.shields<shipShield.Stats.maxShields);
        }
    }
    private void LateUpdate()
    {
        slider.transform.parent.rotation = Quaternion.identity;
    }
}
