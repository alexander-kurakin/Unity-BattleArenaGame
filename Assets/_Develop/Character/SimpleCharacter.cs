using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacter : MonoBehaviour, IDamageable, IDirectionalRotatable, IDirectionalMovable
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _rotationSpeed = 900;
    [SerializeField] private int _maxHealth = 100;

    private bool _isDead = false;

    private Health _health;

    private CharacterController _characterController;
    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    private Vector3 _targetDestination;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 CurrentTarget => _targetDestination;
    public Vector3 CurrentPosition => transform.position;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        _mover = new DirectionalMover(_characterController, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);

        _health = new Health(_maxHealth);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
        _mover.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection)
    {
        throw new System.NotImplementedException();
    }

    public void SetRotationDirection(Vector3 inputDirection)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}