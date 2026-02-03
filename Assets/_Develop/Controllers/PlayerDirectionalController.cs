using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDirectionalController : Controller
{
    private IDirectionalMovable _movable;
    private IKeyboardInput _keyboardInput;
    private float _deadZone = 0.1f;

    public PlayerDirectionalController(IDirectionalMovable movable, IKeyboardInput keyboardInput)
    {
        _movable = movable;
        _keyboardInput = keyboardInput;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        Vector3 input = new Vector3 (_keyboardInput.MoveAxisRaw.x, 0, _keyboardInput.MoveAxisRaw.y);

        if (input.magnitude <= _deadZone || _movable.CanMove == false)
        {
            _movable.SetMoveDirection(Vector3.zero);
            return;
        }

        _movable.SetMoveDirection(input);
    }
}
