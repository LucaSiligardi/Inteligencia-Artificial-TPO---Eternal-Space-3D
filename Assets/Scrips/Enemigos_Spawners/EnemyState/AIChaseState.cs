using UnityEngine;

public class AIChaseState : IAIEnemyState
{
    public void UpdateState(AIEnemy enemy, float deltaTime)
    {
        enemy.transform.Translate(Vector3.back * enemy.aiData.speed * deltaTime, Space.World);

        float newX = Mathf.MoveTowards(
            enemy.transform.position.x,
            enemy.PlayerTransform.position.x,
            enemy.aiData.chaseSpeed * deltaTime
        );
        float newY = Mathf.MoveTowards(
            enemy.transform.position.y,
            enemy.PlayerTransform.position.y,
            enemy.aiData.chaseSpeed * deltaTime
        );

        enemy.transform.position = new Vector3(newX, newY, enemy.transform.position.z);

        if (!enemy.LineOfSight.isInRange(enemy.transform, enemy.PlayerTransform))
        {
            enemy.ChangeState(new AIPatrolState(enemy.transform.position, enemy.aiData.patrolRange));
        }
    }
}