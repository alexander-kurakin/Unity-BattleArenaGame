using UnityEngine;
public class BulletFactory
{
    private readonly Bullet _bulletPrefab;

    public BulletFactory(Bullet bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }

    public Bullet CreateBullet(Vector3 position, Vector3 direction, int damage)
    {
        Bullet bullet = Object.Instantiate(_bulletPrefab, position, Quaternion.identity);

        if (bullet.TryGetComponent<DamageDealingHitBox>(out DamageDealingHitBox hitBox))
            hitBox.SetDamage(damage, true);

        bullet.Launch(direction);

        return bullet;
    }
}