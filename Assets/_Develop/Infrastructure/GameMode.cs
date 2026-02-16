using System;
using UnityEngine;

public class GameMode
{
    public event Action Win;
    public event Action Defeat;

    private IGameCondition _winCondition;
    private IGameCondition _loseCondition;

    private bool _isRunning;

    public bool IsRunning => _isRunning;

    public GameMode(IGameCondition winCondition, IGameCondition loseCondition)
    {
        _winCondition = winCondition;
        _loseCondition = loseCondition;
    }

    public void Start()
    {
        _winCondition.Completed += OnWin;
        _loseCondition.Completed += OnLose;

        _winCondition.Start();
        _loseCondition.Start();

        _isRunning = true;
    }

    public void Update(float deltaTime)
    {
        if (_isRunning == false)
            return;

        _winCondition.Update(deltaTime);
        _loseCondition.Update(deltaTime);
    }

    private void ProcessEndGame()
    {
        _isRunning = false;

        _winCondition.Completed -= OnWin;
        _loseCondition.Completed -= OnLose;

        _winCondition?.Dispose();
        _loseCondition?.Dispose();
    }

    private void OnWin()
    {
        ProcessEndGame();
        Win?.Invoke();
    }

    private void OnLose()
    {
        ProcessEndGame();
        Defeat?.Invoke();
    }
}
