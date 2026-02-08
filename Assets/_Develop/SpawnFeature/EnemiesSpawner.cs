using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner
{
    private EnemiesFactory _enemiesFactory;
    private readonly ReactiveList<SimpleCharacter> _enemiesList = new ReactiveList<SimpleCharacter>();

    public EnemiesSpawner(EnemiesFactory enemiesFactory, ReactiveList<SimpleCharacter> enemiesList)
    {
        _enemiesFactory = enemiesFactory;
        _enemiesList = enemiesList;
    }

    public IEnumerator Spawn(
        EnemyConfig enemyConfig,
        Transform spawnPoint,
        float spawnRadius,
        float spawnTimerValue,
        Func<bool> canSpawn
        )
    {
        while (canSpawn())
        {
            Vector2 randomPositionAroundSpawnPoint = Random.insideUnitCircle * spawnRadius;
            Vector3 offset = new Vector3(randomPositionAroundSpawnPoint.x, 0, randomPositionAroundSpawnPoint.y);
            Vector3 finalPosition = spawnPoint.position + offset;

            SimpleCharacter createdEnemy = _enemiesFactory.CreateEnemy(enemyConfig, spawnPoint.position, finalPosition);

            _enemiesList.Add(createdEnemy);

            yield return new WaitForSeconds(spawnTimerValue);
        }
    }
}
