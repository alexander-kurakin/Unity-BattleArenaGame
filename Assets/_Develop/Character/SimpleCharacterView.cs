using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("isRunning");

    [SerializeField] private Animator _animator;
    [SerializeField] private SimpleCharacter _simpleCharacter;

    private void Update()
    {
        if (_simpleCharacter.CurrentVelocity.magnitude > 0.05f)
            StartRunning();
        else
            StopRunning();
    }

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }
}
