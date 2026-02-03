using UnityEngine;

public class RandomAIDIrectionalMovableController : Controller
{
    private IDirectionalMovable _movable;

    private float _time;
    private float _timeToChangeDirection;

    private Vector3 _inputDirection;

    public RandomAIDIrectionalMovableController(IDirectionalMovable movable, float timeToChangeDirection)
    {
        _movable = movable;
        _timeToChangeDirection = timeToChangeDirection;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _time += deltaTime;

        if (_time >= _timeToChangeDirection)
        {
            _inputDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            _time = 0;
        }

        _movable.SetMoveDirection(_inputDirection);
    }
}