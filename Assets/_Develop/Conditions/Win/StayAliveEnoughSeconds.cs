using System;
using System.Diagnostics;

public class StayAliveEnoughSeconds : IWinCondition
{
    public event Action Completed;

    private float _timeStarted;
    private float _targetSecondsAlive;
    private bool _IsWinEventSent;

    public StayAliveEnoughSeconds(float timeStarted, float targetSecondsAlive)
    {
        _timeStarted = timeStarted;
        _targetSecondsAlive = targetSecondsAlive;
    }

    public void Start()
    {
    }

    public void Update(float deltaTime)
    {
        if ((deltaTime - _timeStarted) >= _targetSecondsAlive)
        {
            if (_IsWinEventSent)
                return;

            Completed?.Invoke();
            _IsWinEventSent = true;
        }

    }

    public void Dispose()
    {
    }
}
