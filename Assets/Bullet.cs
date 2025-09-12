using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > 10) Destroy(gameObject);
    }
}
