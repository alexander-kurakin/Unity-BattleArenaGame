using System;
using Object = UnityEngine.Object;

public class PlayerDied : IGameCondition
{
    public event Action Completed;
    private SimpleCharacter _mainHero;
    private bool _IsLoseEventSent;
    public PlayerDied(SimpleCharacter mainHero)
    {
        _mainHero = mainHero;
    }

    public void Start()
    {
        _mainHero.Died += OnHeroDied;
    }

    private void OnHeroDied()
    {
        if (_IsLoseEventSent == true)
            return;

        Completed?.Invoke();
        _IsLoseEventSent = true;
    }

    public void Update(float deltaTime)
    {
    }

    public void Dispose()
    {
        _mainHero.Died -= OnHeroDied;
        _IsLoseEventSent = false;
    }
}
