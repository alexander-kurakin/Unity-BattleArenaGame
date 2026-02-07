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

    public void Init(ControllersUpdateService controllersUpdateService)
    {
        _controllersUpdateService = controllersUpdateService;
    }

    public IEnumerator Spawn()
    {
        while (_currentEnemiesCount <= _maxEnemiesCount)
        {
            Vector2 randomPositionAroundSpawnPoint = Random.insideUnitCircle * _spawnRadius;
            Vector3 offset = new Vector3(randomPositionAroundSpawnPoint.x, 0, randomPositionAroundSpawnPoint.y);
            Vector3 finalPosition = _spawnPoint.position + offset;

            SimpleCharacter instance = Instantiate(_prefab, finalPosition, Quaternion.identity, null);
            instance.Init();

            Controller controller = new CompositeController(
                new RandomAIDIrectionalMovableController(
                    _spawnPoint.position,
                    _timeToChangeDirection,
                    _leashRadius,
                    _returnLockDuration,
                    instance),
                new PlayerRotatableController(instance, instance)
                );

            controller.Enable();

            _controllersUpdateService.Add( controller );

            _currentEnemiesCount++;

            yield return new WaitForSeconds(_spawnTimerValue);
        }
    }
}
