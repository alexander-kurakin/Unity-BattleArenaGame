using UnityEngine;

public interface IDirectionalMovable
{
    Vector3 CurrentVelocity { get; }
    Vector3 CurrentPosition { get; }
    void SetMoveDirection(Vector3 inputDirection);

    bool CanMove { get; }
}