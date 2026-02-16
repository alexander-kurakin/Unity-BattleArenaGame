public class GameRules
{
    private PlayerProvider _playerProvider;

    public GameRules(PlayerProvider playerProvider)
    {
        _playerProvider = playerProvider;
    }

    public void SetRules(
        LevelConfig levelConfig,
        StayAliveTimerView timerView,
        StayAliveTimer timer)
    {
        bool isTimerNeeded = levelConfig.WinConditionType == WinConditionType.StayAliveEnoughSeconds;

        if (isTimerNeeded)
            timerView?.Init(timer);
        else
            timerView?.Hide();

        bool canHeroBeDamaged = levelConfig.LoseConditionType == LoseConditionType.PlayerDied;
        _playerProvider.MainHero.SetCanBeDamaged(canHeroBeDamaged);
    }
}
