using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable 
{
    void TryShoot(Vector3 direction);
    bool CanShoot { get; }
}
