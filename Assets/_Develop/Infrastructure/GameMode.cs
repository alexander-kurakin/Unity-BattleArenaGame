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
    private ReactiveList<SimpleCharacter> _enemiesList;
    private ReactiveList<Coroutine> _spawnCoroutineList = new ReactiveList<Coroutine>();

    private MonoBehaviour _coroutineRunner;
    private IGameCondition _winCondition;
    private IGameCondition _loseCondition;
    private StayAliveTimer _timer;
    private StayAliveTimerView _stayAliveTimerView;

    private bool _isRunning;

    public GameMode(
        LevelConfig levelConfig, 
        SimpleCharacter mainHero, 
        EnemiesSpawner enemiesSpawner,
        Transform[] enemySpawnerPositions,
        ReactiveList<SimpleCharacter> enemiesList,
        MonoBehaviour coroutineRunner,
        StayAliveTimer timer,
        StayAliveTimerView stayAliveTimerView)
    {
        _levelConfig = levelConfig;
        _mainHero = mainHero;
        _enemiesSpawner = enemiesSpawner;
        _enemySpawnerPositions = enemySpawnerPositions;
        _enemiesList = enemiesList;
        _coroutineRunner = coroutineRunner;
        _timer = timer;
        _stayAliveTimerView = stayAliveTimerView;
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

    private IGameCondition GetWinCondition(WinConditionType winConditionType)
    {
        switch (winConditionType)
        {
            case WinConditionType.KillEnoughEnemies:
                _mainHero.SetShouldShowTimer(false);
                return new KillEnoughEnemies(_levelConfig.TargetEnemiesToKill, _enemiesList);

            case WinConditionType.StayAliveEnoughSeconds:
                _mainHero.SetShouldShowTimer(true);
                _stayAliveTimerView?.Init(_mainHero, _timer);
                return new StayAliveEnoughSeconds(Time.time, _levelConfig.TargetSecondsToSurvive, _timer);

            default: return null;
        }
    }

    private IGameCondition GetLoseCondition(LoseConditionType loseConditionType)
    {
        switch (loseConditionType)
        {
            case LoseConditionType.PlayerDied:
                _mainHero.SetCanBeDamaged(true);
                return new PlayerDied(_mainHero);

            case LoseConditionType.TooMuchEnemiesSpawned:
                _mainHero.SetCanBeDamaged(false);
                return new TooMuchEnemiesSpawned(_levelConfig.TargetMaximumEnemies, _enemiesList);

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
