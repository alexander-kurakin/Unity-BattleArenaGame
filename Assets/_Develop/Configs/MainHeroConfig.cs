using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/MainHeroConfig", fileName = "MainHeroConfig")]
public class MainHeroConfig : ScriptableObject
{
    [field: SerializeField] public SimpleCharacter prefab { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 900;
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;
    [field: SerializeField] public float ShootColdown { get; private set; } = 0.5f;
    [field: SerializeField] public int ProjectileDamage { get; private set; } = 100;
}
