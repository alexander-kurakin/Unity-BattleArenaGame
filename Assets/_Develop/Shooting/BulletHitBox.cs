using UnityEngine;

public class BulletHitBox : MonoBehaviour
{
    private int _damage;

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
