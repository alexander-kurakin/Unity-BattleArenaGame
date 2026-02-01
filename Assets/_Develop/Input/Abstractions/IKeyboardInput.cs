using UnityEngine;

public interface IKeyboardInput
{
    Vector2 MoveAxis { get; }
    bool JumpActionPressed { get; }
}