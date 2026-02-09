using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
       if (TryGetComponent<Rigidbody>(out Rigidbody rigidBody))
            _rigidbody = rigidBody;
    }

    private void Update()
    {
        _rigidbody.AddForce(Vector3.forward * Time.deltaTime * 25f);
    }
}
