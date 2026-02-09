using UnityEngine;
using Object = UnityEngine.Object;

public class Shooter 
{
    private readonly BulletFactory _bulletFactory;
    private Transform _shootPoint;
    private readonly float _cooldown;

    private float _lastShootTime;

    public Shooter(BulletFactory bulletFactory, Transform shootPoint, float cooldown)
    {
        _bulletFactory = bulletFactory;
        _shootPoint = shootPoint;
        _cooldown = cooldown;
    }

    public void TryShoot(Vector3 direction)
    {
        if (Time.time < (_lastShootTime + _cooldown))
            return;

        _lastShootTime = Time.time;

        _bulletFactory.CreateBullet(_shootPoint.position, direction);
    }
}
