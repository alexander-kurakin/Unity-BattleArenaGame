using UnityEngine;
using Object = UnityEngine.Object;

public class Shooter
{
    private BulletFactory _bulletFactory;
    private Transform _shootPoint;
    private float _cooldown;
    private int _projectileDamage;

    private float _lastShootTime;

    public Shooter(BulletFactory bulletFactory, Transform shootPoint, float cooldown, int projectileDamage)
    {
        _bulletFactory = bulletFactory;
        _shootPoint = shootPoint;
        _cooldown = cooldown;
        _projectileDamage = projectileDamage;
    }

    public void TryShoot(Vector3 direction)
    {
        if (Time.time < (_lastShootTime + _cooldown))
            return;

        _lastShootTime = Time.time;

        _bulletFactory.CreateBullet(_shootPoint.position, direction, _projectileDamage);
    }
}
