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

    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();
        _loadingScreen.ShowMessage("Loading ...");

        _controllersUpdateService = new ControllersUpdateService();

        _mainHeroSpawner.Init(_controllersUpdateService);

        foreach (EnemiesSpawner enemiesSpawner in _enemiesSpawners)
            enemiesSpawner.Init(_controllersUpdateService);

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
