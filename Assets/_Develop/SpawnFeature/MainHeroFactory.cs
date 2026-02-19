using System;
using Cinemachine;
using UnityEngine;
using Object = UnityEngine.Object;

public class MainHeroFactory 
{
    public event Action<SimpleCharacter> Created;

    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;
    private BulletFactory _bulletFactory;
    private KeyboardInput _keyboardInput;

    public MainHeroFactory(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory,
        BulletFactory bulletFactory,
        KeyboardInput keyboardInput)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
        _bulletFactory = bulletFactory;
        _keyboardInput = keyboardInput;
    }

    public SimpleCharacter Create(MainHeroConfig config, Vector3 spawnPosition) 
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
            config.ShootColdown,
            config.ProjectileDamage);

        instance.SetShooter(shooter);

        CinemachineVirtualCamera followCameraPrefab = Resources.Load<CinemachineVirtualCamera>("Prefabs/TestCamera");
        CinemachineVirtualCamera followCamera = Object.Instantiate(followCameraPrefab);

        followCamera.Follow = instance.CameraTarget;

        Controller controller = _controllersFactory.CreateMainHeroPlayerController(instance, _keyboardInput);

        controller.Enable();
        _controllersUpdateService.Add(controller, () => instance.IsDestroyed);

        Created?.Invoke(instance);

        return instance;
    }
}

