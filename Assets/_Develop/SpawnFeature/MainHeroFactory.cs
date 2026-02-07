using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class MainHeroFactory 
{
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    public MainHeroFactory(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public SimpleCharacter Create(
        MainHeroConfig config, 
        Vector3 spawnPosition, 
        CinemachineVirtualCamera followCamera, 
        KeyboardInput keyboardInput) 
    {
        SimpleCharacter instance = _charactersFactory.CreateCharacter(
            config.prefab, 
            spawnPosition, 
            config.MoveSpeed, 
            config.RotationSpeed, 
            config.MaxHealth);

        followCamera.Follow = instance.CameraTarget;

        Controller controller = _controllersFactory.CreateMainHeroPlayerController(instance, keyboardInput);

        controller.Enable();
        _controllersUpdateService.Add(controller);

        return instance;
    }
}

