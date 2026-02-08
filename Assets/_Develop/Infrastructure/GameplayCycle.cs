using System;
using System.Collections;
using UnityEngine;

public class GameplayCycle : IDisposable
{
    private MainHeroFactory _mainHeroFactory;
    private LevelConfig _levelConfig;
    private SimpleCharacter _mainHero;
    private KeyboardInput _keyboardInput;
    private ConfirmPopup _confirmPopup;
    private KeyCode _keyToContinue;
    private GameMode _gameMode;
    private EnemiesSpawner _enemiesSpawner;
    private Transform[] _enemySpawnPoints;
    private ReactiveList<SimpleCharacter> _enemiesList = new ReactiveList<SimpleCharacter>();
    private MonoBehaviour _couroutineRunner;

    public GameplayCycle(
        MainHeroFactory mainHeroFactory, 
        LevelConfig levelConfig, 
        KeyboardInput keyboardInput, 
        ConfirmPopup confirmPopup, 
        KeyCode keyToContinue, 
        EnemiesSpawner enemiesSpawner, 
        Transform[] enemySpawnPoints, 
        ReactiveList<SimpleCharacter> enemiesList, 
        MonoBehaviour couroutineRunner)
    {
        _mainHeroFactory = mainHeroFactory;
        _levelConfig = levelConfig;
        _keyboardInput = keyboardInput;
        _confirmPopup = confirmPopup;
        _keyToContinue = keyToContinue;
        _enemiesSpawner = enemiesSpawner;
        _enemySpawnPoints = enemySpawnPoints;
        _enemiesList = enemiesList;
        _couroutineRunner = couroutineRunner;
    }

    public void Prepare()
    {
        _mainHero = _mainHeroFactory.Create(_levelConfig.MainHeroConfig, _levelConfig.MainHeroSpawnPoint, _keyboardInput);
    }

    public IEnumerator Launch()
    {
        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {_keyToContinue.ToString()} to begin");

        yield return _confirmPopup.WaitForConfirm(_keyToContinue);

        _confirmPopup.Hide();

        _gameMode = new GameMode(_levelConfig, _mainHero, _enemiesSpawner, _enemySpawnPoints, _enemiesList, _couroutineRunner);

        _gameMode.Win += OnGameModeWin;
        _gameMode.Defeat += OnGameModeDefeat;

        _gameMode.Start();
    }

    public void Update(float deltaTime) => _gameMode?.Update(deltaTime);

    private void OnGameModeEnded()
    {
        if (_gameMode != null)
        {
            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
        }
    }

    public void Dispose()
    {
        OnGameModeEnded();
    }

    private void OnGameModeDefeat()
    {
        OnGameModeEnded();
        Debug.Log("Defeat");
        _mainHero.ResetHealth();
        _couroutineRunner.StartCoroutine(Launch());
    }

    private void OnGameModeWin()
    {
        OnGameModeEnded();
        Debug.Log("Win");
        _mainHero.ResetHealth();
        _couroutineRunner.StartCoroutine(Launch());
    }
}
