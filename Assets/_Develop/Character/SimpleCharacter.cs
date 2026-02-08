using UnityEngine;

public class SimpleCharacter : MonoBehaviour, IDamageable, IDirectionalRotatable, IDirectionalMovable
{
    [SerializeField] private Transform _cameraTarget;

    private bool _isDead = false;

    private Health _health;

    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Transform CameraTarget => _cameraTarget;
    public Vector3 CurrentPosition => transform.position;

    public void Init(DirectionalMover mover, DirectionalRotator rotator, Health health)
    {
        _mover = mover;
        _rotator = rotator;
        _health = health;

        foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
            initializable.Init();
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
        _mover.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);
    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogError(damage);
            return;
        }

        _health.DecreaseHealth(damage);

        if (_health.HealthIsDrained)
        {
            _isDead = true;
            return;
        }
    }

    public int GetCurrentHealth() => _health.CurrentHealth;
    public bool IsDead() => _isDead;
    public bool CanMove => _isDead == false;

}