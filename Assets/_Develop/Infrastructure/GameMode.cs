using System;
using UnityEngine;

public class GameMode
{
    public event Action Win;
    public event Action Defeat;

    private LevelConfig _levelConfig;

    private SimpleCharacter _mainHero;
    private Transform[] _enemySpawnerPositions;

    private EnemiesSpawner _enemiesSpawner;
    private ReactiveList<SimpleCharacter> _enemiesList = new ReactiveList<SimpleCharacter>();
    private ReactiveList<Coroutine> _spawnCoroutineList = new ReactiveList<Coroutine>();

    private MonoBehaviour _coroutineRunner;
    private IWinCondition _winCondition;
    private ILoseCondition _loseCondition;

    private bool _isRunning;

    public GameMode(
        LevelConfig levelConfig, 
        SimpleCharacter mainHero, 
        EnemiesSpawner enemiesSpawner,
        Transform[] enemySpawnerPositions,
        ReactiveList<SimpleCharacter> enemiesList,
        MonoBehaviour coroutineRunner)
    {
        _levelConfig = levelConfig;
        _mainHero = mainHero;
        _enemiesSpawner = enemiesSpawner;
        _enemySpawnerPositions = enemySpawnerPositions;
        _enemiesList = enemiesList;
        _coroutineRunner = coroutineRunner;
    }

    public void Start()
    {
        _winCondition = GetWinCondition(_levelConfig.WinConditionType);
        _loseCondition = GetLoseCondition(_levelConfig.LoseConditionType);

        _winCondition.Completed += OnWin;
        _loseCondition.Completed += OnLose;

        _winCondition.Start();
        _loseCondition.Start();

        _isRunning = true;

        SpawnEnemies();
    }

    public void Update(float deltaTime)
    {
        if (_isRunning == false)
            return;

        _winCondition.Update(deltaTime);
        _loseCondition.Update(deltaTime);
    }

    private IWinCondition GetWinCondition(WinConditionType winConditionType)
    {
        switch (winConditionType)
        {
            case WinConditionType.KillEnoughEnemies:
                return new KillEnoughEnemies(_levelConfig.TargetEnemiesToKill, _enemiesList);

            case WinConditionType.StayAliveEnoughSeconds:
                return new StayAliveEnoughSeconds(Time.time, _levelConfig.TargetSecondsToSurvive);

            default: return null;
        }
    }

    private ILoseCondition GetLoseCondition(LoseConditionType loseConditionType)
    {
        switch (loseConditionType)
        {
            case LoseConditionType.PlayerDied:
                return new PlayerDied(_mainHero);

            case LoseConditionType.TooMuchEnemiesSpawned:
                return new TooMuchEnemiesSpawned(_levelConfig.TargetEnemiesToKill, _enemiesList);

            default: return null;
        }
    }

    private void SpawnEnemies()
    {
        foreach (Transform enemySpawnerPosition in _enemySpawnerPositions)
        {
            Coroutine spawnCoroutine = _coroutineRunner.StartCoroutine(_enemiesSpawner.Spawn(
                _levelConfig.EnemyConfig,
                enemySpawnerPosition,
                _levelConfig.EnemySpawnRadius,
                _levelConfig.EnemySpawnTimer,
                () => _isRunning
            ));

            _spawnCoroutineList.Add(spawnCoroutine);
        }
    }

    private void ProcessEndGame()
    {
        _isRunning = false;

        foreach (SimpleCharacter enemy in _enemiesList)
            enemy.Destroy();

        _enemiesList.Clear();

        foreach (Coroutine coroutine in _spawnCoroutineList)
            _coroutineRunner.StopCoroutine(coroutine);

        _spawnCoroutineList.Clear();

        _winCondition.Completed -= OnWin;
        _loseCondition.Completed -= OnLose;

        _winCondition?.Dispose();
        _loseCondition?.Dispose();
    }
    private void ProcessDefeat()
    {
        ProcessEndGame();
        Defeat?.Invoke();
    }

    private void ProcessWin()
    {
        ProcessEndGame();
        Win?.Invoke();
    }

    private void OnLose()
    {
        ProcessDefeat();
    }

    private void OnWin()
    {
        ProcessWin();
    }
}
