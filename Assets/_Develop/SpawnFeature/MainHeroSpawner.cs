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

    private KeyboardInput _keyboardInput = new();
    private MainHeroFactory _mainHeroFactory;

    public void Init(
        MainHeroFactory mainHeroFactory)
    { 
        _mainHeroFactory = mainHeroFactory;
    }

    public SimpleCharacter Spawn() => _mainHeroFactory.Create(_prefab, _spawnPoint.position, _followCamera, _keyboardInput);
 

    private void Update()
    {
        _keyboardInput?.Update(Time.deltaTime);
    }
}
