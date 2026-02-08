using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/LevelsListConfig", fileName = "LevelsListConfig")]
public class LevelsListConfig : ScriptableObject
{
    [SerializeField] private List<LevelConfig> _levelConfigs;

    public LevelConfig GetRandom() => _levelConfigs[Random.Range(0, _levelConfigs.Count)];
}
