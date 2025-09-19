using UnityEngine;
using UnityEngine.Rendering;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] float roundsPerSecond = 2;
    [SerializeField] private float fireCD = 0.5f;
    private float bulletSpeed = 10;
    private float nextFire;
    private void Update()
    {
        fireCD = 1 / roundsPerSecond;
        if (Input.GetKey(KeyCode.Mouse0)&&Time.time>nextFire)
        {
            
            GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);

            //bullet.GetComponent<Rigidbody2D>().linearVelocity = transform.parent.GetComponent<Rigidbody2D>().linearVelocity;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.eulerAngles = new Vector3(0,0,RandomUtils.pointAt(transform.position,mousePos));
            bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed,ForceMode2D.Impulse);
            nextFire = Time.time + fireCD;

        }
    }
}
