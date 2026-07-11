using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private float roundsPerSecond = 2;
    [SerializeField] private AudioClip shootSound;
    private float fireCD = 100f;
    private float bulletSpeed = 10;
    private float nextFire;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
        audioSource.clip = shootSound;

        }
    }

    public void Shoot(Vector3 targetPos)
    {
        fireCD = 1 / roundsPerSecond;
        if (Time.time <= nextFire) return;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        bullet.GetComponent<Bullet>().owner = transform.parent.gameObject;
        transform.eulerAngles = new Vector3(0, 0, RandomUtils.pointAt(transform.position, targetPos));
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        nextFire = Time.time + fireCD;
        if (audioSource != null) audioSource.Play();
       
        

    }
}
