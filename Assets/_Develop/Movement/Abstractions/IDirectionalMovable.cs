using UnityEngine;

public interface IDirectionMovable
{
    Vector3 CurrentVelocity { get; }
    void SetMoveDirection(Vector3 inputDirection);
}