using UnityEngine;

public class ObstacleFollower : MonoBehaviour
{
    private Transform _playerTransform;
    private float _offsetX;
    private float _offsetY;
    private float _offsetZ = 5f; // distancia adelante de la nave

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
            _offsetX = transform.position.x - _playerTransform.position.x;
            _offsetY = transform.position.y - _playerTransform.position.y;
        }
    }

    void Update()
    {
        if (_playerTransform != null)
        {
            transform.position = new Vector3(
                _playerTransform.position.x + _offsetX,
                _playerTransform.position.y + _offsetY,
                _playerTransform.position.z + _offsetZ
            );
        }
    }
}