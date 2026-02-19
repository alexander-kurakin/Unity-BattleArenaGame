using System;
using System.Collections;
using UnityEngine;

public class GameplayCycle : IDisposable
{
    private LevelConfig _levelConfig;
    private ConfirmPopup _confirmPopup;
    private KeyCode _keyToContinue;
    private GameMode _gameMode;
    private MonoBehaviour _coroutineRunner;
    private ConditionsFactory _conditionsFactory;
    private GameRules _gameRules;
    private PlayerProvider _playerProvider;
    private MainHeroFactory _mainHeroFactory;
    private EnemiesController _enemiesController;

    public GameplayCycle(
        LevelConfig levelConfig, 
        ConfirmPopup confirmPopup, 
        KeyCode keyToContinue, 
        MonoBehaviour coroutineRunner,
        ConditionsFactory conditionsFactory,
        GameRules gameRules,
        PlayerProvider playerProvider,
        MainHeroFactory mainHeroFactory,
        EnemiesController enemiesController)
    {
        _levelConfig = levelConfig;
        _confirmPopup = confirmPopup;
        _keyToContinue = keyToContinue;
        _coroutineRunner = coroutineRunner;
        _conditionsFactory = conditionsFactory;
        _gameRules = gameRules;
        _playerProvider = playerProvider;
        _mainHeroFactory = mainHeroFactory;
        _enemiesController = enemiesController;
    }

    public void Prepare()
    {
        _mainHeroFactory.Create(_levelConfig.MainHeroConfig, _levelConfig.MainHeroSpawnPoint);
    }

    public IEnumerator Launch()
    {
        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {_keyToContinue.ToString()} to begin");

        yield return _confirmPopup.WaitForConfirm(_keyToContinue);

        _confirmPopup.Hide();

        _gameRules.SetRules();

        IGameCondition winCondition = _conditionsFactory.CreateWinCondition(_levelConfig.WinConditionType);
        IGameCondition loseCondition = _conditionsFactory.CreateLoseCondition(_levelConfig.LoseConditionType);

        _gameMode = new GameMode(winCondition, loseCondition);

        _gameMode.Win += OnGameModeWin;
        _gameMode.Defeat += OnGameModeDefeat;

        _gameMode.Start();

        _enemiesController.StartSpawning(_gameMode);
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

    private void Restart()
    {
        Dispose();
        _gameRules.Cleanup();
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
