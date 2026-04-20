
using UnityEngine;

public interface IAIEnemyState
{
    void UpdateState(AIEnemy enemy, float deltaTime);
}