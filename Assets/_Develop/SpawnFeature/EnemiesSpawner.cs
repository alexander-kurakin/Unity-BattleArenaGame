using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [Header("Spawn settings")]
    [SerializeField] private SimpleCharacter _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private float _spawnTimerValue = 5f;
    [SerializeField ]private int _maxEnemiesCount = 20;
    [Space]
    [Header("Random behaviour settings")]
    [SerializeField] private float _timeToChangeDirection = 2f;
    [SerializeField] private float _leashRadius = 10f;
    [SerializeField] private float _returnLockDuration = 1f; 

    private int _currentEnemiesCount = 0;

    private EnemiesFactory _enemiesFactory;

    public void Init(EnemiesFactory enemiesFactory)
    {
        _enemiesFactory = enemiesFactory;
    }

    public IEnumerator Spawn()
    {
        while (_currentEnemiesCount <= _maxEnemiesCount)
        {
            Vector2 randomPositionAroundSpawnPoint = Random.insideUnitCircle * _spawnRadius;
            Vector3 offset = new Vector3(randomPositionAroundSpawnPoint.x, 0, randomPositionAroundSpawnPoint.y);
            Vector3 finalPosition = _spawnPoint.position + offset;

            _enemiesFactory.CreateEnemy(_prefab, _spawnPoint.position, finalPosition, _timeToChangeDirection, _leashRadius, _returnLockDuration);

            _currentEnemiesCount++;

            yield return new WaitForSeconds(_spawnTimerValue);
        }
    }
}
