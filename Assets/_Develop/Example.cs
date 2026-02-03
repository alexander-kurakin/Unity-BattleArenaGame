using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] private SimpleCharacter _mainHero;

    private Controller _heroController;
    private KeyboardInput _keyboardInput;
    private void Awake()
    {
        _keyboardInput = new KeyboardInput();

        _heroController = new CompositeController(
            new PlayerDirectionalController(_mainHero, _keyboardInput),
            new PlayerRotatableController(_mainHero, _mainHero)
            );

        _heroController.Enable();
    }

    private void Update()
    {
        _heroController.Update(Time.deltaTime);
        _keyboardInput.Update(Time.deltaTime);
    }
}
