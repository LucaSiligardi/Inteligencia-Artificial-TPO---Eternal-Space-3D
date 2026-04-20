using UnityEngine;

public class AIWaitState : IAIEnemyState
{
    private float _waitTime;
    private float _timer;

    public AIWaitState(float waitTime)
    {
        _waitTime = waitTime;
        _timer = 0f;
    }

    public void UpdateState(AIEnemy enemy, float deltaTime)
    {
 
        enemy.transform.Translate(Vector3.back * enemy.aiData.speed * deltaTime, Space.World);


        if (enemy.LineOfSight.isInRange(enemy.transform, enemy.PlayerTransform))
        {
            enemy.ChangeState(new AIChaseState());
            return;
        }

        _timer += deltaTime;
        if (_timer >= _waitTime)
        {
            enemy.ChangeState(new AIPatrolState(enemy.transform.position, enemy.aiData.patrolRange));
        }
    }
}