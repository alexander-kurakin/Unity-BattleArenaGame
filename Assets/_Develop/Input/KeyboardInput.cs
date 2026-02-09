using UnityEngine;

public class KeyboardInput : IKeyboardInput
{
    public Vector2 MoveAxisRaw { get; private set; }

    public bool ActionPressed => Input.GetKeyDown(KeyCode.Space);

    public void Update(float deltaTime)
    {
        MoveAxisRaw = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }
}