using UnityEngine;

public class PowerShot : IShotStrategy
{
    private float spread = 4.0f;

    public void Shoot(Transform firePoint, BulletPool pool)
    {
        // Bala izquierda
        SpawnBullet(firePoint, pool, -firePoint.right * spread);

        // Bala derecha
        SpawnBullet(firePoint, pool, firePoint.right * spread);
    }

    private void SpawnBullet(Transform firePoint, BulletPool pool, Vector3 offset)
    {
        GameObject bullet = pool.GetBullet();
        bullet.transform.position = firePoint.position + offset;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);

        // Cambios visuales
        bullet.transform.localScale = Vector3.one * 1.5f;

        var mr = bullet.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.material.color = Color.red;
        }
    }
    public float GetCooldown()
    {
        return 3f;
    }
}