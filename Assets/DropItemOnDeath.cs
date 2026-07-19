using UnityEngine;

public class DropItemOnDeath : MonoBehaviour
{
    private Entity entity;
    [SerializeField] GameObject item;
    private void Start()
    {
        entity = GetComponent<Entity>();
        entity.OnDeath += Entity_OnDeath;
    }

    private void Entity_OnDeath(object sender, System.EventArgs e)
    {
        Instantiate(item, transform.position, Quaternion.identity, null);
    }
}
