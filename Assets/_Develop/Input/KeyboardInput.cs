using UnityEngine;

public class KeyboardInput : IKeyboardInput
{
    public Vector2 MoveAxis { get; private set; }

    public bool JumpActionPressed => Input.GetKeyDown(KeyCode.Space);

    public void Update(float deltaTime)
    {
        MoveAxis = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }
}