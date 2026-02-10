using System;
using UnityEngine;

public class SimpleCharacter : MonoDestroyable, IDamageable, IDirectionalRotatable, IDirectionalMovable, IShootable
{
    [SerializeField] private Transform _cameraTarget;

    public event Action Died;
    private bool _isDead = false;
    private bool _isDeadEventSent;
    private int _maxHealth;

    private Health _health;
    private DirectionalMover _mover;
    private DirectionalRotator _rotator;
    private Shooter _shooter;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Transform CameraTarget => _cameraTarget;
    public Vector3 CurrentPosition => transform.position;

    public bool CanBeDamaged {get; private set;}
    public bool ShouldShowTimer { get; private set;}

    public void Init(DirectionalMover mover, DirectionalRotator rotator, Health health)
    {
        _mover = mover;
        _rotator = rotator;
        _health = health;

        _maxHealth = _health.CurrentHealth;

        foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
            initializable.Init();
    }
    public void SetShooter(Shooter shooter)
    {
        _shooter = shooter;
    }

    public void SetCanBeDamaged(bool state)
    { 
        CanBeDamaged = state;
    }

    public void SetShouldShowTimer(bool state)
    {
        ShouldShowTimer = state;
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
        _mover.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);
    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public void TryShoot(Vector3 direction)
    {
        if (CanShoot == false)
            return;

        _shooter?.TryShoot(direction);
    }

    public void TakeDamage(int damage)
    {
        if (CanBeDamaged == false) return;

        if (damage < 0)
        {
            Debug.LogError(damage);
            return;
        }

        _health.DecreaseHealth(damage);

        if (_health.HealthIsDrained)
        {
            _isDead = true;

            if (_isDeadEventSent)
                return;
            
            Died?.Invoke();
            _isDeadEventSent = true;

            return;
        }
    }

    public int GetCurrentHealth() => _health.CurrentHealth;
    public bool CanMove => _isDead == false;
    public bool CanShoot => _isDead == false && _shooter != null;
    public Health GetHealth() => _health;

}