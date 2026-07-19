using UnityEngine;

public class OnDeathActivate : MonoBehaviour
{
    [SerializeField] private Entity entity;

    private void Start()
    {
        entity.OnDeath += Entity_OnDeath;
        gameObject.SetActive(false);
    }

    private void Entity_OnDeath(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
