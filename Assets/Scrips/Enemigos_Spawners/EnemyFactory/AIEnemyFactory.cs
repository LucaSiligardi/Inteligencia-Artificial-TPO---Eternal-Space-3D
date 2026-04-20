using UnityEngine;

public class AIEnemyFactory : MonoBehaviour
{
    public GameObject aiEnemyPrefab;

    public GameObject CreateAIEnemy(Vector3 position)
    {
        return Instantiate(aiEnemyPrefab, position, Quaternion.identity);
    }
}