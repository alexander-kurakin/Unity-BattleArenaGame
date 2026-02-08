using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptValuesView : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    private LevelConfig _levelConfig;

    private void Awake()
    {
        _levelConfig = Resources.Load<LevelConfig>("Configs/LevelConfig");
    }

    private void Start()
    {
        _text.text =
            "Враги появляются каждые " + _levelConfig.EnemySpawnTimer.ToString() + " с \n" +
            "Враги появляются в радиусе = " + _levelConfig.EnemySpawnRadius.ToString() + "\n" +
            "Активное условие победы = " + _levelConfig.WinConditionType.ToString() + "\n" +
            "Активное условие поражения = " + _levelConfig.LoseConditionType.ToString() + "\n" +
            "TargetEnemiesToKill: " + _levelConfig.TargetEnemiesToKill.ToString() + "\n" +
            "TargetMaximumEnemies: " + _levelConfig.TargetMaximumEnemies.ToString() + "\n" +
            "TargetSecondsToSurvive: " + _levelConfig.TargetSecondsToSurvive.ToString();
    }
}
