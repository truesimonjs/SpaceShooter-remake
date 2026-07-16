using System.Collections;
using UnityEngine;

public class EntityDisplay : MonoBehaviour
{
   private Color normalColor;
   private SpriteRenderer sprite;
   [SerializeField] private Entity entity;
    private void Start()
    {
        if (entity == null) entity = gameObject.GetComponent<Entity>();
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        normalColor = sprite.color;
        entity.OnTakeDamage += Entity_OnTakeDamage;
        
    }

    private void Entity_OnTakeDamage(object sender, System.EventArgs e)
    {
        StartCoroutine(DamageEffect());
    }

    public IEnumerator DamageEffect()
    {
        


        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = normalColor;

    }

}
