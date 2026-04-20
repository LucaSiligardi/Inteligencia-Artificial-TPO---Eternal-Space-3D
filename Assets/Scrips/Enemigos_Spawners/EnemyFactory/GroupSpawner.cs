using System.Collections.Generic;
using UnityEngine;

public class GroupSpawner : MonoBehaviour
{
    public EnemyFactory factory;
    public float spawnEvery = 2f;

  
    public List<Vector3> spawnPoints = new List<Vector3>
    {
        new Vector3(0, 5, 0),    // arriba centro
        new Vector3(0, -5, 0),   // abajo centro
        new Vector3(-5, 3, 0),   // arriba izquierda
        new Vector3(5, 3, 0),    // arriba derecha
        new Vector3(-5, -3, 0),  // abajo izquierda
        new Vector3(5, -3, 0),   // abajo derecha
    };

    private float _timer;

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            SpawnGroup();
            _timer = spawnEvery;
        }
    }

    void SpawnGroup()
    {
        
        List<Vector3> shuffled = new List<Vector3>(spawnPoints);
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            Vector3 temp = shuffled[i];
            shuffled[i] = shuffled[rand];
            shuffled[rand] = temp;
        }

       
        for (int i = 0; i < 2; i++)
        {
            Vector3 worldPos = transform.position + shuffled[i];
            factory.CreateEnemy(worldPos);
        }
    }
}