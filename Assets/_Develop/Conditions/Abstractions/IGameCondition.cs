using System;

public interface IGameCondition : IDisposable
{
    public event Action Completed;
    void Start();
    void Update(float deltaTime);
}
