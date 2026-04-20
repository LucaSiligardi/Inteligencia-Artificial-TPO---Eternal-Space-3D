using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private AudioClip normalShotSound;
    [SerializeField] private AudioClip powerShotSound;

    private IShotStrategy currentStrategy;
    private float lastShootTime;

    private UIManager uiManager;

    private void Start()
    {
        currentStrategy = new NormalShot();
        lastShootTime = -Mathf.Infinity;

        uiManager = FindObjectOfType<UIManager>();

        // Inicializar UI
        if (uiManager != null)
            uiManager.UpdateShotUI(false);
    }
    private void Update()
    {
        float cooldown = currentStrategy.GetCooldown();
        float timeSinceLastShot = Time.time - lastShootTime;

       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (timeSinceLastShot >= cooldown)
            {
                currentStrategy.Shoot(firePoint, bulletPool);

               
                if (AudioManager.Instance != null)
                {
                    if (currentStrategy is NormalShot)
                        AudioManager.Instance.PlaySFX(normalShotSound);
                    else
                        AudioManager.Instance.PlaySFX(powerShotSound);
                }

                lastShootTime = Time.time;

            
                if (uiManager != null)
                    uiManager.UpdateCooldown(0f);
            }
        }

      
        float normalized = Mathf.Clamp01(timeSinceLastShot / cooldown);

        if (uiManager != null)
        {
            uiManager.UpdateCooldown(normalized);
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentStrategy is NormalShot)
            {
                currentStrategy = new PowerShot();
                if (uiManager != null) uiManager.UpdateShotUI(true);
            }
            else
            {
                currentStrategy = new NormalShot();
                if (uiManager != null) uiManager.UpdateShotUI(false);
            }
        }
    }
}