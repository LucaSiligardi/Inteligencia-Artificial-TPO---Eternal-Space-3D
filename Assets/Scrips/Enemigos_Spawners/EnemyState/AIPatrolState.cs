using UnityEngine;

public class AIPatrolState : IAIEnemyState
{
    private float _pointAx;
    private float _pointBx;
    private float _currentTargetX;
    private float _patrolTimer;
    private float _patrolDuration;

    public AIPatrolState(Vector3 origin, float range, float patrolDuration = 5f)
    {
        _pointAx = origin.x + range;
        _pointBx = origin.x - range;
        _currentTargetX = _pointBx;
        _patrolDuration = patrolDuration;
        _patrolTimer = 0f;
    }

    public void UpdateState(AIEnemy enemy, float deltaTime)
    {
        enemy.transform.Translate(Vector3.back * enemy.aiData.speed * deltaTime, Space.World);

        float newX = Mathf.MoveTowards(
            enemy.transform.position.x,
            _currentTargetX,
            enemy.aiData.patrolSpeed * deltaTime
        );
        enemy.transform.position = new Vector3(newX, enemy.transform.position.y, enemy.transform.position.z);

        if (Mathf.Abs(enemy.transform.position.x - _currentTargetX) < 0.1f)
        {
            _currentTargetX = (_currentTargetX == _pointAx) ? _pointBx : _pointAx;
        }

 
 
        if (enemy.LineOfSight.isInRange(enemy.transform, enemy.PlayerTransform)
            && enemy.LineOfSight.isInAngle(enemy.transform, enemy.PlayerTransform)
            && enemy.LineOfSight.hasLineOfSight(enemy.transform, enemy.PlayerTransform))
        {
            enemy.ChangeState(new AIChaseState());
            return;
        }

     
        _patrolTimer += deltaTime;
        if (_patrolTimer >= _patrolDuration)
        {
            enemy.ChangeState(new AIWaitState(enemy.aiData.waitTime));
        }
    }
}