using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent2Spawner : MonoBehaviour
{
    [SerializeField] private GameObject agent2Prefab;
    public float spawnEvery = 15f;
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
        if (_playerTransform == null) return;
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            Vector3 spawnPos = new Vector3(
                _playerTransform.position.x,
                _playerTransform.position.y,
                _playerTransform.position.z + spawnDistanceAhead
            );
            Instantiate(agent2Prefab, spawnPos, Quaternion.identity);
            _timer = spawnEvery;
        }
    }
}
