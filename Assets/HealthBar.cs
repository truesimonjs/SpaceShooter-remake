using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if ( entity == null) entity = GetComponent<Entity>();
        if (slider == null) slider = GetComponentInChildren<Slider>();
        slider.maxValue = entity.MaxHealth;
        entity.OnHealthChange += Entity_HealthChange;

    }

    private void Entity_HealthChange(object sender, System.EventArgs e)
    {
        
       
        slider.value = entity.Health;
        slider.gameObject.SetActive(entity.Health < entity.MaxHealth);
    }
    private void LateUpdate()
    {
        slider.transform.parent.rotation = Quaternion.identity;
    }
}
