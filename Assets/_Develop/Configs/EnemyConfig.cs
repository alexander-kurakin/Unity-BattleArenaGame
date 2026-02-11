using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/EnemyConfig", fileName = "EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public SimpleCharacter prefab { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 900;
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;

    [field: SerializeField] public float TimeToChangeDirection { get; private set; } = 2f;
    [field: SerializeField] public float LeashRadius { get; private set; } = 10f;
    [field: SerializeField] public float ReturnLockDuration { get; private set; } = 1f;
    [field: SerializeField] public int DamageToHero { get; private set; } = 25;
}
