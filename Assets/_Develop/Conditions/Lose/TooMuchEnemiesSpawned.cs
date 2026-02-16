using System;

public class TooMuchEnemiesSpawned : IGameCondition
{
    public event Action Completed;
    private bool _IsLoseEventSent;

    private int _currentEnemies = 0;
    private int _targetEnemies;
    private IReadOnlyReactiveList<SimpleCharacter> _enemyList;

    public TooMuchEnemiesSpawned(int targetEnemies, IReadOnlyReactiveList<SimpleCharacter> enemyList)
    {
        _targetEnemies = targetEnemies;
        _enemyList = enemyList;
    }

    public void Start()
    {
        _enemyList.Added += OnEnemyAdded;
    }

    private void OnEnemyAdded(SimpleCharacter character)
    {
        _currentEnemies++;
    }

    public void Update(float deltaTime)
    {
        if (_currentEnemies >= _targetEnemies)
        {
            if (_IsLoseEventSent == true)
                return;

            Completed?.Invoke();
            _IsLoseEventSent = true;
        }
    }

    public void Dispose()
    {
        _enemyList.Added -= OnEnemyAdded;
        _IsLoseEventSent = false;
        _currentEnemies = 0;
    }

}
