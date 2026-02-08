using System;
using TMPro;
using UnityEngine;

public class MainHeroHealthView : MonoBehaviour, IInitializable
{
    [SerializeField] TMP_Text _text;
    private SimpleCharacter _mainHero;
    private bool _isInit;

    private void OnHealthChanged()
    {
        _text.text = _mainHero.GetCurrentHealth().ToString();
    }

    public void Init()
    {
        _mainHero = GetComponentInParent<SimpleCharacter>();
        _mainHero.GetHealth().Changed += OnHealthChanged;
        _isInit = true;
    }

    private void OnDestroy()
    {
        _mainHero.GetHealth().Changed -= OnHealthChanged;
    }
}
