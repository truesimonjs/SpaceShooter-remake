using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private float roundsPerSecond = 2;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private float maxAngle = 45;
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
        //cooldown
        fireCD = 1 / roundsPerSecond;
        if (Time.time <= nextFire) return;
        float targetRotation = RandomUtils.pointAt(transform.position, targetPos);
        float rotation = Mathf.MoveTowardsAngle(0, targetRotation, maxAngle);
        
        transform.localEulerAngles = new Vector3(0, 0, rotation);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().owner = transform.parent.gameObject;
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        nextFire = Time.time + fireCD;
        if (audioSource != null) audioSource.Play();
       
        

    }
}
