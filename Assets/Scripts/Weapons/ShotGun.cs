using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField]protected int bulletAmount = 6;
    [SerializeField]protected float spreadPerBullet = 30;

    protected override void FireBullet(Vector3 targetPos)
    {
        
        float targetRotation = RandomUtils.pointAt(transform.position, targetPos);
        float rotation = Mathf.MoveTowardsAngle(0, targetRotation, maxAngle);
        transform.localEulerAngles = new Vector3(0, 0, rotation);
        for (int i = 0; i < bulletAmount; i++)
        {
           
            float spread = -bulletAmount*spreadPerBullet/2 + i * spreadPerBullet;
            Bullet bullet = SpawnBullet();
            bullet.transform.Rotate(Vector3.forward * spread);
            bullet.Move(bullet.transform.up, bulletSpeed);
        }
     
    }
}
