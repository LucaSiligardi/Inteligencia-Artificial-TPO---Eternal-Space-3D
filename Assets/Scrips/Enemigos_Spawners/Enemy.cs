using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptable enemyData;
    private IEnemyState currentState;
    [SerializeField] private AudioClip deathSound;
    public float Speed => enemyData.speed;

    void Start()
    {
        Debug.Log("Enemy se MUEVE");
        currentState = new EnemyMovingState();
    }

    void Update()
    {
        currentState?.UpdateState(this, Time.deltaTime);
        
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState = newState;
    }

    public void Die()
    {
        Debug.Log("Enemy MUERE");

        GameMananger.Instance.AddPoints(enemyData.pointsOnDeath);

     
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(deathSound);
        }

        ChangeState(new EnemyDeadState());
        gameObject.SetActive(false);
    }


}