using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ConfirmPopup _confirmPopup;

    [SerializeField] private KeyCode _keyToContinue = KeyCode.F;

    [Header("Enemy Spawn settings")]
    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] private Transform[] _enemySpawnPoints;

    [SerializeField] private float _enemySpawnRadius = 10f;
    [SerializeField] private float _enemySpawnTimerValue = 5f;
    [SerializeField] private int _enemyMaxEnemiesCount = 20;

    [Header("Main Hero Spawn settings")]
    [SerializeField] private MainHeroConfig _mainHeroConfig;
    [SerializeField] private Transform _mainHeroSpawnPoint;
    [SerializeField] private CinemachineVirtualCamera _mainHeroFollowCamera;

    private ControllersUpdateService _controllersUpdateService;
    private KeyboardInput _keyboardInput;

    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();
        _loadingScreen.ShowMessage("Loading ...");

        _controllersUpdateService = new ControllersUpdateService();
        _keyboardInput = new KeyboardInput();

        ControllersFactory controllersFactory = new ControllersFactory();
        CharactersFactory charactersFactory = new CharactersFactory();

        EnemiesFactory enemiesFactory = new EnemiesFactory(_controllersUpdateService, controllersFactory, charactersFactory);
        MainHeroFactory mainHeroFactory = new MainHeroFactory(_controllersUpdateService, controllersFactory, charactersFactory);

        EnemiesSpawner enemiesSpawner = new EnemiesSpawner(enemiesFactory);
        
        yield return new WaitForSeconds(1.5f);

        SimpleCharacter mainHero = mainHeroFactory.Create(_mainHeroConfig, _mainHeroSpawnPoint.position, _mainHeroFollowCamera, _keyboardInput);

        _loadingScreen.Hide();
        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {_keyToContinue.ToString()} to begin");

        yield return _confirmPopup.WaitForConfirm(_keyToContinue);

        _confirmPopup.Hide();

        foreach (Transform enemiesSpawnPosition in _enemySpawnPoints)
            StartCoroutine(enemiesSpawner.Spawn(_enemyConfig, enemiesSpawnPosition, _enemyMaxEnemiesCount, _enemySpawnRadius, _enemySpawnTimerValue));
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
        _keyboardInput?.Update(Time.deltaTime);
    }
}
