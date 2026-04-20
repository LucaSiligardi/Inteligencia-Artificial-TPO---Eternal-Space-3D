using UnityEngine;

[CreateAssetMenu(fileName = "AIEnemyData", menuName = "Enemies/AIEnemyData")]
public class AIEnemyScriptable : ScriptableObject
{
    public float speed = 3f;
    public float chaseSpeed = 6f;
    public int pointsOnDeath = 50;
    public float patrolRange = 5f;
    public float patrolSpeed = 2f;
    public float waitTime = 2f;
    public float patrolDuration = 5f; 
}