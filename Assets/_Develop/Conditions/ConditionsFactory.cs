using UnityEngine;

public class ConditionsFactory
{
    private PlayerProvider _playerProvider;

    public ConditionsFactory(PlayerProvider playerProvider)
    {
        _playerProvider = playerProvider;
    }

    public IGameCondition CreateWinCondition(
        WinConditionType winConditionType,
        LevelConfig levelConfig,
        IReadOnlyReactiveList<SimpleCharacter> enemiesList,
        StayAliveTimer timer)
    {
        switch (winConditionType)
        {
            case WinConditionType.KillEnoughEnemies:
                return new KillEnoughEnemies(levelConfig.TargetEnemiesToKill, enemiesList);

            case WinConditionType.StayAliveEnoughSeconds:
                return new StayAliveEnoughSeconds(Time.time, levelConfig.TargetSecondsToSurvive, timer);

            default: return null;
        }
    }

    public IGameCondition CreateLoseCondition(
        LoseConditionType loseConditionType,
        LevelConfig levelConfig,
        IReadOnlyReactiveList<SimpleCharacter> enemiesList)
    {
        switch (loseConditionType)
        {
            case LoseConditionType.PlayerDied:
                return new PlayerDied(_playerProvider.MainHero);

            case LoseConditionType.TooMuchEnemiesSpawned:
                return new TooMuchEnemiesSpawned(levelConfig.TargetMaximumEnemies, enemiesList);

            default: return null;
        }
    }
}
