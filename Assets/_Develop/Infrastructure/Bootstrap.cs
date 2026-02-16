using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("UI settings")]
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ConfirmPopup _confirmPopup;
    [SerializeField] private ReactiveListView _reactiveListView;
    [SerializeField] private StayAliveTimerView _stayAliveTimerView;

    [SerializeField] private KeyCode _keyToContinue = KeyCode.F;

    [Header("Enemy Spawn settings")]
    [SerializeField] private Transform[] _enemySpawnPoints;

    [Header("Shooting settings")]
    [SerializeField] private Bullet _bulletPrefab;

    private ControllersUpdateService _controllersUpdateService;
    private KeyboardInput _keyboardInput;
    private GameplayCycle _gameplayCycle;

    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();
        _loadingScreen.ShowMessage("Loading ...");

        LevelsListConfig levelsListConfig = Resources.Load<LevelsListConfig>("Configs/LevelsListConfig");

        _controllersUpdateService = new ControllersUpdateService();
        _keyboardInput = new KeyboardInput();

        ControllersFactory controllersFactory = new ControllersFactory();
        CharactersFactory charactersFactory = new CharactersFactory();
        BulletFactory bulletFactory = new BulletFactory(_bulletPrefab);

        EnemiesFactory enemiesFactory = new EnemiesFactory(_controllersUpdateService, controllersFactory, charactersFactory);
        MainHeroFactory mainHeroFactory = new MainHeroFactory(_controllersUpdateService, controllersFactory, charactersFactory, bulletFactory);

        ReactiveList<SimpleCharacter> enemiesList = new ReactiveList<SimpleCharacter>();

        EnemiesSpawner enemiesSpawner = new EnemiesSpawner(enemiesFactory, enemiesList);

        LevelConfig levelConfig = levelsListConfig.GetRandom();

        StayAliveTimer stayAliveTimer = new StayAliveTimer();
        ConditionsFactory conditionsFactory = new ConditionsFactory();
        GameRules gameRules = new GameRules();

        _gameplayCycle = new GameplayCycle(
            mainHeroFactory,
            levelConfig,
            _keyboardInput,
            _confirmPopup,
            _keyToContinue,
            enemiesSpawner,
            _enemySpawnPoints,
            enemiesList,
            this,
            _stayAliveTimerView,
            stayAliveTimer,
            conditionsFactory,
            gameRules);
            
        yield return new WaitForSeconds(1.5f);

        _gameplayCycle.Prepare();

        _loadingScreen.Hide();

        _reactiveListView.Init(enemiesList);

        yield return _gameplayCycle.Launch();
    }

    private void OnDestroy()
    {
        _gameplayCycle?.Dispose();
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
        _keyboardInput?.Update(Time.deltaTime);
        _gameplayCycle?.Update(Time.deltaTime);
    }
}
