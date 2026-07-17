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

        //shield
        if ( shipShield == null) shipShield = transform.parent.GetComponentInChildren<ShipShieldModule>(true);
        if (shipShield != null)
        {
            shipShield.OnShieldChange += ShipShield_OnShieldChange;
            shieldImage.fillAmount = shipShield.shields / shipShield.maxShields;
        }
        else shieldImage.fillAmount = 0;
        updateBar();
    }

    private void ShipShield_OnShieldChange(object sender, System.EventArgs e)
    {
        shieldImage.fillAmount = shipShield.shields / shipShield.maxShields;
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
            slider.gameObject.SetActive(entity.Health < entity.MaxHealth ||shipShield.shields<shipShield.maxShields);
        }
    }
    private void LateUpdate()
    {
        slider.transform.parent.rotation = Quaternion.identity;
    }
}
