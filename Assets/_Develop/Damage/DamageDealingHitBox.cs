using UnityEngine;

public class DamageDealingHitBox : MonoBehaviour
{
    private int _damage;
    private bool _shouldDestroyOnHit;

    public void SetDamage(int damage, bool shouldDestroyOnHit = false)
    {
        _damage = damage;
        _shouldDestroyOnHit = shouldDestroyOnHit;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        damageable?.TakeDamage(_damage);

        if (_shouldDestroyOnHit)
            Destroy(gameObject);
    }
}