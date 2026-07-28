using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private float roundsPerSecond = 2;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] protected float maxAngle = 45;
    [SerializeField]protected float bulletSpeed = 10;
    [SerializeField] float range = 50;
    private float fireCD { get => 1 / roundsPerSecond; }
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
        if (!enabled) return;
        if (Time.time < nextFire) return; ;
        FireBullet(targetPos);
        nextFire = Time.time + fireCD;
        if (audioSource != null) audioSource.Play();
    }
    

    protected virtual void FireBullet(Vector3 targetPos)
    {
        float targetRotation = RandomUtils.pointAt(transform.position, targetPos);
        float rotation = Mathf.MoveTowardsAngle(transform.parent.eulerAngles.z, targetRotation, maxAngle);
        transform.eulerAngles = new Vector3(0, 0, rotation);
        Bullet bullet = SpawnBullet();
        bullet.Move(bullet.transform.up, bulletSpeed);
    }
    protected virtual Bullet SpawnBullet ()
    {
        
        GameObject bulletGameobject = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bulletComp = bulletGameobject.GetComponent<Bullet>();
        bulletComp.owner = transform.parent.gameObject;
        bulletComp.range = range;
        return bulletComp;
    }
}
