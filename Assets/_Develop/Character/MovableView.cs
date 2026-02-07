using UnityEngine;

public class MovableView : MonoBehaviour, IInitializable
{
    private readonly int IsRunningKey = Animator.StringToHash("isRunning");

    [SerializeField] private Animator _animator;
    private IMovable _movable;
    private bool _isInit;

    private void Update()
    {
        if (_isInit == false)
            return;

        if (_movable.CurrentVelocity.magnitude > 0.05f)
            StartRunning();
        else
            StopRunning();
    }

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }

    public void Init()
    {
        _movable = GetComponentInParent<IMovable>();
        _isInit = true;
    }
}
