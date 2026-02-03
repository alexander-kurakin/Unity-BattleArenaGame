using UnityEngine;

public class PlayerRotatableController : Controller
{
    private IDirectionalRotatable _rotatable;
    private IDirectionalMovable _movable;

    public PlayerRotatableController(IDirectionalRotatable rotatable, IDirectionalMovable movable)
    {
        _rotatable = rotatable;
        _movable = movable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _rotatable.SetRotationDirection(_movable.CurrentVelocity);
    }
}