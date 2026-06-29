using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent2 : MonoBehaviour
{
    public SteeringAgent2Scriptable data;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private string playerTag = "Player";

    private IAgent2State _currentState;
    private Transform _playerTransform;

    public Transform PlayerTransform => _playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindWithTag(playerTag);
        if (player != null)
            _playerTransform = player.transform;
        else
            Debug.LogWarning("SteeringAgent2: No se encontró objeto con tag Player");

        _currentState = new Agent2WanderState(transform.forward);
    }

    void Update()
    {
        if (_playerTransform == null) return;
        _currentState?.UpdateState(this, Time.deltaTime);
    }

    public void ChangeState(IAgent2State newState)
    {
        _currentState = newState;
    }

    public void Die()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(deathSound);
        GameMananger.Instance.AddPoints(data.pointsOnDeath);
        gameObject.SetActive(false);
    }
}