using UnityEngine;

public class ConditionsFactory
{
    private PlayerProvider _playerProvider;
    private LevelConfig _levelConfig;
    private IReadOnlyReactiveList<SimpleCharacter> _enemiesList;
    private StayAliveTimer _timer;

    public ConditionsFactory(
        PlayerProvider playerProvider,
        LevelConfig levelConfig,
        IReadOnlyReactiveList<SimpleCharacter> enemiesList,
        StayAliveTimer timer)
    {
        _playerProvider = playerProvider;
        _levelConfig = levelConfig;
        _enemiesList = enemiesList;
        _timer = timer;
    }

    public IGameCondition CreateWinCondition(WinConditionType type)
    {
        switch (type)
        {
            case WinConditionType.KillEnoughEnemies:
                return new KillEnoughEnemies(_levelConfig.TargetEnemiesToKill, _enemiesList);

            case WinConditionType.StayAliveEnoughSeconds:
                return new StayAliveEnoughSeconds(Time.time, _levelConfig.TargetSecondsToSurvive, _timer);

            default: return null;
        }
    }

    public IGameCondition CreateLoseCondition(LoseConditionType type)
    {
        switch (type)
        {
            case LoseConditionType.PlayerDied:
                return new PlayerDied(_playerProvider.MainHero);

            case LoseConditionType.TooMuchEnemiesSpawned:
                return new TooMuchEnemiesSpawned(_levelConfig.TargetMaximumEnemies, _enemiesList);

            default: return null;
        }
    }
}
