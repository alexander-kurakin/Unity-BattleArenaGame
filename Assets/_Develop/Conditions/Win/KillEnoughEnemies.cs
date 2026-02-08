using System;

public class KillEnoughEnemies : IWinCondition
{
    public event Action Completed;
    private bool _IsWinEventSent;

    private int _currentKills = 0;
    private int _targetKills;
    private ReactiveList<SimpleCharacter> _enemyList = new ReactiveList<SimpleCharacter>();

    public KillEnoughEnemies(int targetKills, ReactiveList<SimpleCharacter> enemyList)
    {
        _targetKills = targetKills;
        _enemyList = enemyList;
    }

    public void Start()
    {
        _enemyList.Removed += OnEnemyKilled;
    }

    private void OnEnemyKilled(SimpleCharacter character)
    {
        _currentKills++;
    }

    public void Update(float deltaTime)
    {
        if (_currentKills >= _targetKills)
        {
            if (_IsWinEventSent)
                return;

            Completed?.Invoke();
            _IsWinEventSent = true;
        }
    }

    public void Dispose()
    {
        _enemyList.Removed -= OnEnemyKilled;
        _IsWinEventSent = false;
        _currentKills = 0;
    }
}
