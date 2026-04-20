using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovingState : IEnemyState
{
    [SerializeField] private EnemyScriptable enemyData;
    public void UpdateState(Enemy enemy, float deltaTime)
    {
        enemy.transform.Translate(Vector3.back * enemy.enemyData.speed * deltaTime, Space.World);

    }
}
