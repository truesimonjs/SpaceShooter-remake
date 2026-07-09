using UnityEngine;

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
        if (Input.GetKey(KeyCode.Mouse0))
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Shoot(mousePos);
            

        }
    }

    private void Shoot(Vector3 targetPos)
    {
        if (Time.time <= nextFire) return; 
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        bullet.GetComponent<Bullet>().owner = transform.parent.gameObject;
        transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, targetPos));
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        nextFire = Time.time + fireCD;


    }
}
