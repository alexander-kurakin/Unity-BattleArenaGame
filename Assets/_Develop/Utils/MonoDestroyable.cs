using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoDestroyable : MonoBehaviour
{
    public event Action<MonoDestroyable> Destroyed;

    public bool IsDestroyed { get; private set; }

    public void Destroy()
    { 
        Destroy(gameObject); 

        IsDestroyed = true;
        Destroyed?.Invoke(this);
    }
}
