using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner
{
    private int _currentEnemiesCount = 0;
    private EnemiesFactory _enemiesFactory;

    public EnemiesSpawner(EnemiesFactory enemiesFactory)
    {
        _enemiesFactory = enemiesFactory;
    }

    public IEnumerator Spawn(
        EnemyConfig enemyConfig,
        Transform spawnPoint,
        float maxEnemiesCount,
        float spawnRadius,
        float spawnTimerValue
        )
    {
        while (_currentEnemiesCount <= maxEnemiesCount)
        {
            Vector2 randomPositionAroundSpawnPoint = Random.insideUnitCircle * spawnRadius;
            Vector3 offset = new Vector3(randomPositionAroundSpawnPoint.x, 0, randomPositionAroundSpawnPoint.y);
            Vector3 finalPosition = spawnPoint.position + offset;

            _enemiesFactory.CreateEnemy(enemyConfig, spawnPoint.position, finalPosition);

            _currentEnemiesCount++;

            yield return new WaitForSeconds(spawnTimerValue);
        }
    }
}
