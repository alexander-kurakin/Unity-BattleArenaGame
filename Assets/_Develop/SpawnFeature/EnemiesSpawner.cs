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

    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    public void Init(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public IEnumerator Spawn()
    {
        while (_currentEnemiesCount <= _maxEnemiesCount)
        {
            Vector2 randomPositionAroundSpawnPoint = Random.insideUnitCircle * _spawnRadius;
            Vector3 offset = new Vector3(randomPositionAroundSpawnPoint.x, 0, randomPositionAroundSpawnPoint.y);
            Vector3 finalPosition = _spawnPoint.position + offset;

            SimpleCharacter instance = _charactersFactory.CreateCharacter(_prefab, finalPosition, 5, 900, 100);

            Controller controller = _controllersFactory.CreateEnemyController(
                _spawnPoint.position,
                _timeToChangeDirection,
                _leashRadius,
                _returnLockDuration,
                instance);
                
            controller.Enable();

            _controllersUpdateService.Add( controller );

            _currentEnemiesCount++;

            yield return new WaitForSeconds(_spawnTimerValue);
        }
    }
}
