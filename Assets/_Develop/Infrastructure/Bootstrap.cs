using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MainHeroSpawner _mainHeroSpawner;
    [SerializeField] private EnemiesSpawner[] _enemiesSpawners;

    private void Awake()
    {
        SimpleCharacter mainHero = _mainHeroSpawner.Spawn();
        
        foreach (EnemiesSpawner enemiesSpawner in _enemiesSpawners) 
            StartCoroutine(enemiesSpawner.Spawn());
    }
}
