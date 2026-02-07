using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    Vector3 CurrentVelocity { get; }
    bool CanMove { get; }
}
