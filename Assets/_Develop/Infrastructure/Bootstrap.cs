using System.Collections;
using Cinemachine;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("UI settings")]
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ConfirmPopup _confirmPopup;

    [SerializeField] private KeyCode _keyToContinue = KeyCode.F;

    [Header("Enemy Spawn settings")]
    [SerializeField] private Transform[] _enemySpawnPoints;

    [SerializeField] private float _enemySpawnRadius = 10f;
    [SerializeField] private float _enemySpawnTimerValue = 5f;
    [SerializeField] private int _enemyMaxEnemiesCount = 20;

    [Header("Main Hero Spawn settings")]
    [SerializeField] private Transform _mainHeroSpawnPoint;

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

        MainHeroConfig heroConfig = Resources.Load<MainHeroConfig>("Configs/MainHeroConfig");
        EnemyConfig enemyConfig = Resources.Load<EnemyConfig>("Configs/EnemyConfig");

        _controllersUpdateService = new ControllersUpdateService();
        _keyboardInput = new KeyboardInput();

        ControllersFactory controllersFactory = new ControllersFactory();
        CharactersFactory charactersFactory = new CharactersFactory();

        EnemiesFactory enemiesFactory = new EnemiesFactory(_controllersUpdateService, controllersFactory, charactersFactory);
        MainHeroFactory mainHeroFactory = new MainHeroFactory(_controllersUpdateService, controllersFactory, charactersFactory);

        EnemiesSpawner enemiesSpawner = new EnemiesSpawner(enemiesFactory);
        
        yield return new WaitForSeconds(1.5f);

        SimpleCharacter mainHero = mainHeroFactory.Create(heroConfig, _mainHeroSpawnPoint.position, _keyboardInput);

        _loadingScreen.Hide();
        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {_keyToContinue.ToString()} to begin");

        yield return _confirmPopup.WaitForConfirm(_keyToContinue);

        _confirmPopup.Hide();

        foreach (Transform enemiesSpawnPosition in _enemySpawnPoints)
            StartCoroutine(enemiesSpawner.Spawn(enemyConfig, enemiesSpawnPosition, _enemyMaxEnemiesCount, _enemySpawnRadius, _enemySpawnTimerValue));
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
        _keyboardInput?.Update(Time.deltaTime);
    }
}
