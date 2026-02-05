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

    public SimpleCharacter Spawn()
    {
        SimpleCharacter instance = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, null);
        _followCamera.Follow = instance.CameraTarget;

        _keyboardInput = new KeyboardInput();

        _heroController = new CompositeController(
            new PlayerDirectionalController(instance, _keyboardInput),
            new PlayerRotatableController(instance, instance)
            );

        _heroController.Enable();

        return instance;
    }

    private void Update()
    {
        _heroController?.Update(Time.deltaTime);
        _keyboardInput?.Update(Time.deltaTime);
    }
}
