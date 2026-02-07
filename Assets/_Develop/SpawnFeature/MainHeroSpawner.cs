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

    private KeyboardInput _keyboardInput;
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    public void Init(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory)
    { 
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public SimpleCharacter Spawn()
    {
        SimpleCharacter instance = _charactersFactory.CreateCharacter(_prefab, _spawnPoint.position, 5, 900, 100);

        _followCamera.Follow = instance.CameraTarget;

        _keyboardInput = new KeyboardInput();

        Controller controller = _controllersFactory.CreateMainHeroPlayerController(instance, _keyboardInput);

        controller.Enable();
        _controllersUpdateService.Add(controller);

        return instance;
    }

    private void Update()
    {
        _keyboardInput?.Update(Time.deltaTime);
    }
}
