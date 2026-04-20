using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = GetComponentInParent<PlayerHealth>();
        if (_playerHealth == null)
            _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            AIEnemy aiEnemy = other.GetComponent<AIEnemy>();
            if (aiEnemy != null)
            {
                _playerHealth.TakeDamage(1);
                aiEnemy.DestroyWithoutPoints();
            }
        }
    }
}