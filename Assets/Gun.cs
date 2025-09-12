using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private float fireCD = 0.5f;
    private float nextFire;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)&&Time.time>nextFire)
        {
            
            GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 3,ForceMode2D.Impulse);
            nextFire = Time.time + fireCD;

        }
    }
}
