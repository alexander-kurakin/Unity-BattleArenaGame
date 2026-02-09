using UnityEngine;
public class BulletFactory
{
    private readonly Bullet _bulletPrefab;

    public BulletFactory(Bullet bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }

    public Bullet CreateBullet(Vector3 position, Vector3 direction)
    {
        Bullet bullet = Object.Instantiate(_bulletPrefab, position, Quaternion.identity);
        bullet.Launch(direction);

        return bullet;
    }
}