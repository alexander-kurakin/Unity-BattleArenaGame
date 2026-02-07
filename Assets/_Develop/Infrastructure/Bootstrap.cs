using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MainHeroSpawner _mainHeroSpawner;
    [SerializeField] private EnemiesSpawner[] _enemiesSpawners;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ConfirmPopup _confirmPopup;

    [SerializeField] private KeyCode _keyToContinue = KeyCode.F;

    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;
    EnemiesFactory _enemiesFactory;
    MainHeroFactory _mainHeroFactory;

    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();
        _loadingScreen.ShowMessage("Loading ...");

        _controllersUpdateService = new ControllersUpdateService();
        _controllersFactory = new ControllersFactory();
        _charactersFactory = new CharactersFactory();

        _enemiesFactory = new EnemiesFactory(_controllersUpdateService,_controllersFactory, _charactersFactory);
        _mainHeroFactory = new MainHeroFactory(_controllersUpdateService, _controllersFactory, _charactersFactory);

        _mainHeroSpawner.Init(_mainHeroFactory);

        foreach (EnemiesSpawner enemiesSpawner in _enemiesSpawners)
            enemiesSpawner.Init(_enemiesFactory);

        yield return new WaitForSeconds(1.5f);

        SimpleCharacter mainHero = _mainHeroSpawner.Spawn();

        _loadingScreen.Hide();
        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {_keyToContinue.ToString()} to begin");

        yield return _confirmPopup.WaitForConfirm(_keyToContinue);

        _confirmPopup.Hide();

        foreach (EnemiesSpawner enemiesSpawner in _enemiesSpawners)
            StartCoroutine(enemiesSpawner.Spawn());
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
    }
}
