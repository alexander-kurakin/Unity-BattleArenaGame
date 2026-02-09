using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootableController : Controller
{
    private readonly IShootable _shootable;
    private readonly Transform _shootTransform;
    private readonly IKeyboardInput _keyboardInput;

    public PlayerShootableController(IShootable shootable, Transform shootTransform, IKeyboardInput keyboardInput)
    {
        _shootable = shootable;
        _shootTransform = shootTransform;
        _keyboardInput = keyboardInput;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_shootable.CanShoot == false || _keyboardInput.ActionPressed == false)
            return;

        Vector3 direction = _shootTransform.forward;
        _shootable.TryShoot(direction);
    }
}
