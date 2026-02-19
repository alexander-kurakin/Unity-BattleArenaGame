public class GameRules
{
    private PlayerProvider _playerProvider;
    private LevelConfig _levelConfig;
    private StayAliveTimerView _stayAliveTimerView;
    private StayAliveTimer _timer;

    public GameRules(
        PlayerProvider playerProvider,
        LevelConfig levelConfig,
        StayAliveTimerView stayAliveTimerView,
        StayAliveTimer timer)
    {
        _playerProvider = playerProvider;
        _levelConfig = levelConfig;
        _stayAliveTimerView = stayAliveTimerView;
        _timer = timer;
    }

    public void Cleanup()
    {
        _stayAliveTimerView?.Hide();
    }

    public void SetRules()
    {
        bool isTimerNeeded = _levelConfig.WinConditionType == WinConditionType.StayAliveEnoughSeconds;

        if (isTimerNeeded)
            _stayAliveTimerView?.Init(_timer);
        else
            _stayAliveTimerView?.Hide();

        bool canHeroBeDamaged = _levelConfig.LoseConditionType == LoseConditionType.PlayerDied;
        _playerProvider.MainHero.SetCanBeDamaged(canHeroBeDamaged);
    }
}
