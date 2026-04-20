using UnityEngine;

public class NormalShot : IShotStrategy
{
    public void Shoot(Transform firePoint, BulletPool pool)
    {
        GameObject bullet = pool.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);

        // Visual normal
        bullet.transform.localScale = Vector3.one;
        var mr = bullet.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.material.color = Color.white;
        }
    }

    public float GetCooldown()
    {
        return 1f;
    }
}