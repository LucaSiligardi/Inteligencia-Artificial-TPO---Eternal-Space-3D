using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public AIEnemyScriptable aiData;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private string playerTag = "Player";

    private IAIEnemyState _currentState;
    private LineOfSight _lineOfSight;
    private Transform _playerTransform;

 
    public LineOfSight LineOfSight => _lineOfSight;
    public Transform PlayerTransform => _playerTransform;

    void Start()
    {
        _lineOfSight = GetComponent<LineOfSight>();

        GameObject player = GameObject.FindWithTag(playerTag);
        if (player != null)
            _playerTransform = player.transform;

      
        _currentState = new AIPatrolState(transform.position, aiData.patrolRange);
    }

    void Update()
    {
        if (_playerTransform == null) return;
        _currentState?.UpdateState(this, Time.deltaTime);
    }

    public void ChangeState(IAIEnemyState newState)
    {
        _currentState = newState;
    }

    public void Die()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(deathSound);

        GameMananger.Instance.AddPoints(aiData.pointsOnDeath);
        gameObject.SetActive(false);
    }
    public void DestroyWithoutPoints()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(deathSound);
        gameObject.SetActive(false);
    }
}