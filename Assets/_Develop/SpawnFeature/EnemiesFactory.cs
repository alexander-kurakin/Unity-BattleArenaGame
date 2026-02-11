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
        EnemyConfig enemyConfig,
        Vector3 spawnPosition,
        Vector3 finalPosition)
    {
        SimpleCharacter instance = _charactersFactory.CreateCharacter(
            enemyConfig.prefab, 
            finalPosition, 
            enemyConfig.MoveSpeed, 
            enemyConfig.RotationSpeed, 
            enemyConfig.MaxHealth);

        Controller controller = _controllersFactory.CreateEnemyController(
            spawnPosition,
            enemyConfig.TimeToChangeDirection,
            enemyConfig.LeashRadius,
            enemyConfig.ReturnLockDuration,
            instance);

        controller.Enable();

        _controllersUpdateService.Add(controller, () => instance.IsDestroyed);

        instance.SetCanBeDamaged(true);

        instance.GetComponentInChildren<DamageDealingHitBox>()?.SetDamage(enemyConfig.DamageToHero);

        return instance;
    }
}
