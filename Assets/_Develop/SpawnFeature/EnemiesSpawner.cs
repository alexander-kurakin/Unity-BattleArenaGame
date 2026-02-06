using System.Collections;
using System.Collections.Generic;
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

    private Controller _enemyController;
    private List<Controller> _controllers = new();

    private int _currentEnemiesCount = 0;
    
    public IEnumerator Spawn()
    {
        while (_currentEnemiesCount <= _maxEnemiesCount)
        {
            Vector2 randomPositionAroundSpawnPoint = Random.insideUnitCircle * _spawnRadius;
            Vector3 offset = new Vector3(randomPositionAroundSpawnPoint.x, 0, randomPositionAroundSpawnPoint.y);
            Vector3 finalPosition = _spawnPoint.position + offset;

            SimpleCharacter instance = Instantiate(_prefab, finalPosition, Quaternion.identity, null);

            instance.Init();

            _enemyController = new CompositeController(
                new RandomAIDIrectionalMovableController(
                    _spawnPoint.position,
                    _timeToChangeDirection,
                    _leashRadius,
                    _returnLockDuration,
                    instance),
                new PlayerRotatableController(instance, instance)
                );

            _enemyController.Enable();

            _controllers.Add(_enemyController);

            _currentEnemiesCount++;

            yield return new WaitForSeconds(_spawnTimerValue);
        }
    }

    private void Update()
    {
        foreach(Controller controller in  _controllers)
            controller.Update(Time.deltaTime);
    }
}
