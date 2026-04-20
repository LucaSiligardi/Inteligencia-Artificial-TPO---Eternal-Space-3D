using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyFactory factory;
    public float spawnEvery = 2f;
    float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            factory.CreateEnemy(transform.position); 
            timer = spawnEvery;
        }
    }
}
