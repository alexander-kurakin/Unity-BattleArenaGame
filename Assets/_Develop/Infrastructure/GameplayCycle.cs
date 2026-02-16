using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCycle : IDisposable
{
    private LevelConfig _levelConfig;
    private ConfirmPopup _confirmPopup;
    private KeyCode _keyToContinue;
    private GameMode _gameMode;
    private EnemiesSpawner _enemiesSpawner;
    private Transform[] _enemySpawnPoints;
    private ReactiveList<SimpleCharacter> _enemiesList;
    private MonoBehaviour _coroutineRunner;
    private StayAliveTimerView _stayAliveTimerView;
    private StayAliveTimer _timer;
    private ConditionsFactory _conditionsFactory;
    private GameRules _gameRules;
    private PlayerProvider _playerProvider;
    private List<Coroutine> _spawnCoroutines = new List<Coroutine>();

    public GameplayCycle(
        LevelConfig levelConfig, 
        ConfirmPopup confirmPopup, 
        KeyCode keyToContinue, 
        EnemiesSpawner enemiesSpawner, 
        Transform[] enemySpawnPoints, 
        ReactiveList<SimpleCharacter> enemiesList, 
        MonoBehaviour coroutineRunner,
        StayAliveTimerView stayAliveTimerView,
        StayAliveTimer timer,
        ConditionsFactory conditionsFactory,
        GameRules gameRules,
        PlayerProvider playerProvider)
    {
        _levelConfig = levelConfig;
        _confirmPopup = confirmPopup;
        _keyToContinue = keyToContinue;
        _enemiesSpawner = enemiesSpawner;
        _enemySpawnPoints = enemySpawnPoints;
        _enemiesList = enemiesList;
        _coroutineRunner = coroutineRunner;
        _stayAliveTimerView = stayAliveTimerView;
        _timer = timer;
        _conditionsFactory = conditionsFactory;
        _gameRules = gameRules;
        _playerProvider = playerProvider;
    }

    public void Prepare()
    {
        _playerProvider.Create(_levelConfig.MainHeroConfig, _levelConfig.MainHeroSpawnPoint);
    }

    public IEnumerator Launch()
    {
        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {_keyToContinue.ToString()} to begin");

        yield return _confirmPopup.WaitForConfirm(_keyToContinue);

        _confirmPopup.Hide();

        _gameRules.SetRules(_levelConfig, _stayAliveTimerView, _timer);

        IGameCondition winCondition = _conditionsFactory.CreateWinCondition(
            _levelConfig.WinConditionType, _levelConfig, _enemiesList, _timer);

        IGameCondition loseCondition = _conditionsFactory.CreateLoseCondition(
            _levelConfig.LoseConditionType, _levelConfig, _enemiesList);

        _gameMode = new GameMode(winCondition, loseCondition);

        _gameMode.Win += OnGameModeWin;
        _gameMode.Defeat += OnGameModeDefeat;

        _gameMode.Start();

        SpawnEnemies();
    }

    public void Update(float deltaTime) => _gameMode?.Update(deltaTime);

    public void Dispose()
    {
        if (_gameMode != null)
        {
            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
        }
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in _enemySpawnPoints)
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

    private void Restart()
    {
        Dispose();
        StopSpawning();
        CleanupEnemies();
        _stayAliveTimerView?.Hide();

        _playerProvider.DestroyHero();
        Prepare();

        _coroutineRunner.StartCoroutine(Launch());
    }

    private void OnGameModeDefeat()
    {
        Debug.Log("Defeat! " + _levelConfig.LoseConditionType);
        Restart();
    }

    private void OnGameModeWin()
    {
        Debug.Log("Win " + _levelConfig.WinConditionType);
        Restart();
    }
}
