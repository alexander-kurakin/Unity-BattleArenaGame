using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 25f;
    [SerializeField] private float _lifeTime = 3f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody rigidBody))
            _rigidbody = rigidBody;
    }

    public void Launch(Vector3 direction)
    {
        direction.y = 0;
        direction.Normalize();

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        _rigidbody.velocity = direction * _speed;
        Destroy(gameObject, _lifeTime);
    }
}
