using UnityEngine;

public interface IShotStrategy
{
    void Shoot(Transform firePoint, BulletPool pool);
    float GetCooldown();
}