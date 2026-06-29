using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class Agent3 : MonoBehaviour
{
    public Agent3Scriptable data;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private string playerTag = "Player";

    private Transform _playerTransform;
    private List<Vector3> _path;
    private int _currentWaypointIndex;
    private float _pathUpdateTimer;

    void Start()
    {
        GameObject player = GameObject.FindWithTag(playerTag);
        if (player != null)
            _playerTransform = player.transform;

        _path = new List<Vector3>();
        _pathUpdateTimer = 0f;
        UpdatePath();
    }

    void Update()
    {
        if (_playerTransform == null) return;

        // Avanzar hacia la cámara igual que los demás asteroides
        transform.Translate(Vector3.back * data.speed * Time.deltaTime, Space.World);

        // Actualizar el path cada cierto tiempo
        _pathUpdateTimer -= Time.deltaTime;
        if (_pathUpdateTimer <= 0f)
        {
            UpdatePath();
            _pathUpdateTimer = data.pathUpdateRate;
        }

        // Seguir el camino con Arrive
        FollowPath();
    }

    void UpdatePath()
    {
        if (Pathfinding.Instance == null) return;
        List<Vector3> newPath = Pathfinding.Instance.FindPath(
            transform.position,
            _playerTransform.position
        );
        if (newPath != null)
        {
            _path = newPath;
            _currentWaypointIndex = 0;
        }
    }

    void FollowPath()
    {
        if (_path == null || _path.Count == 0) return;
        if (_currentWaypointIndex >= _path.Count) return;

        Vector3 target = _path[_currentWaypointIndex];

        
        Vector3 direction = SteeringBehaviours.Arrive(
            transform,
            target,
            data.arriveSlowRadius
        );

        
        Vector3 movement = new Vector3(direction.x, direction.y, 0f)
            * data.speed * Time.deltaTime;
        transform.position += movement;

        if (Vector3.Distance(transform.position, target)
            < data.waypointReachedDistance)
        {
            _currentWaypointIndex++;
        }
    }
    void OnDrawGizmos()
    {
        if (_path == null) return;
        Gizmos.color = Color.green;
        for (int i = 0; i < _path.Count - 1; i++)
        {
            Gizmos.DrawLine(_path[i], _path[i + 1]);
            Gizmos.DrawSphere(_path[i], 0.3f);
        }
    }
    public void Die()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(deathSound);
        GameMananger.Instance.AddPoints(data.pointsOnDeath);
        gameObject.SetActive(false);
    }
}