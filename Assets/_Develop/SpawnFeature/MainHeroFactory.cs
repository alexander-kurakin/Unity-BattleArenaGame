using Cinemachine;
using UnityEngine;

public class MainHeroFactory 
{
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;
    private BulletFactory _bulletFactory;

    public MainHeroFactory(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory,
        BulletFactory bulletFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
        _bulletFactory = bulletFactory;
    }

    public SimpleCharacter Create(
        MainHeroConfig config, 
        Vector3 spawnPosition, 
        KeyboardInput keyboardInput) 
    {
        SimpleCharacter instance = _charactersFactory.CreateCharacter(
            config.prefab, 
            spawnPosition, 
            config.MoveSpeed, 
            config.RotationSpeed, 
            config.MaxHealth);

        ShootPoint shootPoint = instance.GetComponentInChildren<ShootPoint>();

        Shooter shooter = new Shooter(
            _bulletFactory,
            shootPoint.transform,
            config.ShootColdown);

        instance.SetShooter(shooter);

        CinemachineVirtualCamera followCameraPrefab = Resources.Load<CinemachineVirtualCamera>("Prefabs/TestCamera");
        CinemachineVirtualCamera followCamera = Object.Instantiate(followCameraPrefab);

        followCamera.Follow = instance.CameraTarget;

        Controller controller = _controllersFactory.CreateMainHeroPlayerController(instance, keyboardInput);

        controller.Enable();
        _controllersUpdateService.Add(controller, () => instance.IsDestroyed);

        return instance;
    }
}

