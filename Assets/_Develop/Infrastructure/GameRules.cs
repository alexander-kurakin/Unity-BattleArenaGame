public class GameRules
{
    public void SetRules(
        LevelConfig levelConfig,
        SimpleCharacter mainHero,
        StayAliveTimerView timerView,
        StayAliveTimer timer)
    {
        bool isTimerNeeded = levelConfig.WinConditionType == WinConditionType.StayAliveEnoughSeconds;

        if (isTimerNeeded)
            timerView?.Init(timer);
        else
            timerView?.Hide();

        bool canHeroBeDamaged = levelConfig.LoseConditionType == LoseConditionType.PlayerDied;
        mainHero.SetCanBeDamaged(canHeroBeDamaged);
    }
}
