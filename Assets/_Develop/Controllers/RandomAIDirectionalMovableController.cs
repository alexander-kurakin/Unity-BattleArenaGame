using UnityEngine;

public class RandomAIDIrectionalMovableController : Controller
{
    private const float SimilarityThreshold = 0.85f;
    private const int MaxRerolls = 8;
    private const float MinMagnitude = 0.15f;

    private readonly Vector3 _spawnPoint;
    private readonly float _timeToChangeDirection;
    private readonly float _leashRadius;
    private readonly float _returnLockDuration;

    private Vector3 _currentDirection;
    private Vector3 _previousDirection;
    private IDirectionalMovable _movable;
    
    private float _time;
    private float _returnLockTimer;
    
    public RandomAIDIrectionalMovableController(
        Vector3 spawnPoint, 
        float timeToChangeDirection, 
        float leashRadius,
        float returnLockDuration,
        IDirectionalMovable movable)
    {
        _spawnPoint = spawnPoint;
        _timeToChangeDirection = timeToChangeDirection;
        _leashRadius = leashRadius;
        _returnLockDuration = returnLockDuration;
        _movable = movable;

        _currentDirection = GetNewDirection(Vector3.forward);
        _previousDirection = _currentDirection;
    }

    private Vector3 GetNewDirection(Vector3 previousDirection)
    {
        Vector3 direction = Vector3.zero;

        for (int i = 0; i < MaxRerolls; i++)
        {
            direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

            if (direction.magnitude < MinMagnitude)
                continue;

            direction.Normalize();

            if (previousDirection.sqrMagnitude < 0.001f)
                return direction;

            float similarity = Vector3.Dot(previousDirection.normalized, direction);

            if (similarity > SimilarityThreshold)
                continue;

            return direction;
        }

        return direction.sqrMagnitude > 0.001f ? direction.normalized : Vector3.forward;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        Vector3 currentOffsetFromSpawn = _movable.CurrentPosition - _spawnPoint;

        _returnLockTimer -= Time.deltaTime;

        bool isOutsideLeash = currentOffsetFromSpawn.magnitude > _leashRadius;

        if (isOutsideLeash)
            _returnLockTimer = _returnLockDuration;

        if (_returnLockTimer > 0f)
        {
            Vector3 returnDirection = (-currentOffsetFromSpawn).normalized;
            _movable.SetMoveDirection(returnDirection);
            return;
        }

        _time += deltaTime;

        if (_time >= _timeToChangeDirection)
        {
            _previousDirection = _currentDirection;
            _currentDirection = GetNewDirection(_previousDirection);
            _time = 0;
        }

        _movable.SetMoveDirection(_currentDirection);
    }
}