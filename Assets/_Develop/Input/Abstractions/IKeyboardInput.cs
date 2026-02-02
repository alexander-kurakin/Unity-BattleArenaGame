using UnityEngine;

public interface IKeyboardInput
{
    Vector2 MoveAxisRaw { get; }
    bool JumpActionPressed { get; }
}