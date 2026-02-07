using UnityEngine;

public class EnemiesFactory
{
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    public EnemiesFactory(
        ControllersUpdateService controllersUpdateService, 
        ControllersFactory controllersFactory, 
        CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public SimpleCharacter CreateEnemy(
        SimpleCharacter prefab,
        Vector3 spawnPosition,
        Vector3 finalPosition,
        float timeToChangeDirection,
        float leashRadius,
        float returnLockDuration
        )
    {

        SimpleCharacter instance = _charactersFactory.CreateCharacter(prefab, finalPosition, 5, 900, 100);

        Controller controller = _controllersFactory.CreateEnemyController(
            spawnPosition,
            timeToChangeDirection,
            leashRadius,
            returnLockDuration,
            instance);

        controller.Enable();

        _controllersUpdateService.Add(controller);

        return instance;
    }
}
