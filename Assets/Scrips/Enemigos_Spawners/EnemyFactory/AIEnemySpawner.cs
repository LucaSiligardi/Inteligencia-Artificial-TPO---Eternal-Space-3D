using UnityEngine;

public class AIEnemySpawner : MonoBehaviour
{
    public AIEnemyFactory factory;
    public float spawnEvery = 10f;
    public float spawnDistanceAhead = 50f; 
    private float _timer;
    private Transform _playerTransform;

    void Start()
    {
        _timer = spawnEvery;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            _playerTransform = player.transform;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            Vector3 spawnPos = _playerTransform != null
                ? new Vector3(
                    _playerTransform.position.x,
                    _playerTransform.position.y,
                    _playerTransform.position.z + spawnDistanceAhead)
                : transform.position;

            factory.CreateAIEnemy(spawnPos);
            _timer = spawnEvery;
        }
    }
}