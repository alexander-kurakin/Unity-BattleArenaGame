using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : IDisposable
{
    private EnemiesSpawner _enemiesSpawner;
    private ReactiveList<SimpleCharacter> _enemiesList;
    private Transform[] _spawnPoints;
    private MonoBehaviour _coroutineRunner;
    private LevelConfig _levelConfig;
    private GameMode _gameMode;
    private List<Coroutine> _spawnCoroutines = new List<Coroutine>();

    public EnemiesController(
        EnemiesSpawner enemiesSpawner,
        ReactiveList<SimpleCharacter> enemiesList,
        Transform[] spawnPoints,
        MonoBehaviour coroutineRunner,
        LevelConfig levelConfig)
    {
        _enemiesSpawner = enemiesSpawner;
        _enemiesList = enemiesList;
        _spawnPoints = spawnPoints;
        _coroutineRunner = coroutineRunner;
        _levelConfig = levelConfig;
    }

    public void StartSpawning(GameMode gameMode)
    {
        _gameMode = gameMode;
        _gameMode.Win += OnGameplayEnded;
        _gameMode.Defeat += OnGameplayEnded;

        foreach (Transform spawnPoint in _spawnPoints)
        {
            Coroutine spawnCoroutine = _coroutineRunner.StartCoroutine(_enemiesSpawner.Spawn(
                _levelConfig.EnemyConfig,
                spawnPoint,
                _levelConfig.EnemySpawnRadius,
                _levelConfig.EnemySpawnTimer,
                () => _gameMode != null && _gameMode.IsRunning
            ));

            _spawnCoroutines.Add(spawnCoroutine);
        }
    }

    private void OnGameplayEnded()
    {
        StopSpawning();
        CleanupEnemies();
        Dispose();
    }

    private void StopSpawning()
    {
        foreach (Coroutine coroutine in _spawnCoroutines)
            _coroutineRunner.StopCoroutine(coroutine);

        _spawnCoroutines.Clear();
    }

    private void CleanupEnemies()
    {
        foreach (SimpleCharacter enemy in _enemiesList)
            enemy.Destroy();

        _enemiesList.Clear();
    }

    public void Dispose()
    {
        if (_gameMode != null)
        {
            _gameMode.Win -= OnGameplayEnded;
            _gameMode.Defeat -= OnGameplayEnded;
            _gameMode = null;
        }
    }
}
