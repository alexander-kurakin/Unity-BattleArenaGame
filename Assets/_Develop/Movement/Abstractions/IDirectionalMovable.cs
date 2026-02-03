using UnityEngine;

public interface IDirectionalMovable
{
    Vector3 CurrentVelocity { get; }
    void SetMoveDirection(Vector3 inputDirection);

    bool CanMove { get; }
}