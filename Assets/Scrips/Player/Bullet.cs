using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private float lifeTime = 3f; 
    private float timer;

    void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
       
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
                ReturnToPool();
                return;
            }

            AIEnemy aiEnemy = other.GetComponent<AIEnemy>();
            if (aiEnemy != null)
            {
                aiEnemy.Die();
                ReturnToPool();
                return;
            }

            SteeringAgent2 agent2 = other.GetComponent<SteeringAgent2>();
            if (agent2 != null)
            {
                agent2.Die();
                ReturnToPool();
                return;
            }
            Agent3 agent3 = other.GetComponent<Agent3>();
            if (agent3 != null)
            {
                agent3.Die();
                ReturnToPool();
                return;
            }
            ReturnToPool();

        }

    }

    private void ReturnToPool()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }
}