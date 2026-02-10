using System;
using UnityEngine;

public class StayAliveEnoughSeconds : IWinCondition
{
    public event Action Completed;

    private float _timeStarted;
    private float _targetSecondsAlive;
    private bool _isWinEventSent;
    private StayAliveTimer _timer;

    public StayAliveEnoughSeconds(float timeStarted, float targetSecondsAlive, StayAliveTimer timer)
    {
        _timeStarted = timeStarted;
        _targetSecondsAlive = targetSecondsAlive;
        _timer = timer;
    }

    public void Start()
    {
    }

    public void Update(float deltaTime)
    {
        float elapsedTime = Time.time - _timeStarted;
        _timer.SetElapsedTime(elapsedTime);

        if (elapsedTime >= _targetSecondsAlive)
        {
            if (_isWinEventSent)
                return;

            Completed?.Invoke();
            _isWinEventSent = true;
        }
    }

    public void Dispose()
    {
    }
}
