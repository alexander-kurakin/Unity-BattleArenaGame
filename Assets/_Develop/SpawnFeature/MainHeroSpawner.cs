using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MainHeroSpawner : MonoBehaviour
{
    [SerializeField] private SimpleCharacter _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CinemachineVirtualCamera _followCamera;

    private Controller _heroController;
    private KeyboardInput _keyboardInput;
    private ControllersUpdateService _controllersUpdateService;

    public void Init(ControllersUpdateService controllersUpdateService)
    { 
        _controllersUpdateService = controllersUpdateService;
    }

    public SimpleCharacter Spawn()
    {
        SimpleCharacter instance = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, null);
        instance.Init();

        _followCamera.Follow = instance.CameraTarget;

        _keyboardInput = new KeyboardInput();

        Controller controller = new CompositeController(
            new PlayerDirectionalController(instance, _keyboardInput),
            new PlayerRotatableController(instance, instance)
            );

        controller.Enable();
        _controllersUpdateService.Add(controller);

        return instance;
    }

    private void Update()
    {
        _keyboardInput?.Update(Time.deltaTime);
    }
}
