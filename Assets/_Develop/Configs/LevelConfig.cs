using UnityEngine;
[CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public EnemyConfig EnemyConfig { get; private set; }
    [field: SerializeField] public MainHeroConfig MainHeroConfig { get; private set; }
    [field: SerializeField] public Vector3 MainHeroSpawnPoint { get; private set; }
    [field: SerializeField] public float EnemySpawnRadius { get; private set; }
    [field: SerializeField] public float EnemySpawnTimer { get; private set; }
    [field: SerializeField] public WinConditionType WinConditionType { get; private set; }
    [field: SerializeField] public LoseConditionType LoseConditionType { get; private set; }
    [field: SerializeField] public int TargetEnemiesToKill { get; private set; }
    [field: SerializeField] public float TargetSecondsToSurvive { get; private set; }
    [field: SerializeField] public int TargetMaximumEnemies { get; private set; }


    [ContextMenu("UpdateStartHeroPosition")]
    private void UpdateStartHeroPosition()
    {
        GameObject point = GameObject.FindGameObjectWithTag("StartHeroPosition");
        MainHeroSpawnPoint = point.transform.position;
    }
}
