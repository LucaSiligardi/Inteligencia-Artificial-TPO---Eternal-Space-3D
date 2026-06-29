using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent3Spawner : MonoBehaviour
{
    [SerializeField] private GameObject agent3Prefab;
    public float spawnEvery = 20f;
    public float spawnDistanceAhead = 40f;
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
                _playerTransform.position.x + Random.Range(-5f, 5f),
                _playerTransform.position.y + Random.Range(-3f, 3f),
                _playerTransform.position.z + spawnDistanceAhead
            );
            Instantiate(agent3Prefab, spawnPos, Quaternion.identity);
            _timer = spawnEvery;
        }
    }
}