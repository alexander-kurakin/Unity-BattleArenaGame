using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReactiveListView : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    private ReactiveList<SimpleCharacter> _enemiesList = new ReactiveList<SimpleCharacter>();
    private int _killCount = 0;

    public void Init(ReactiveList<SimpleCharacter> enemiesList)
    {
        _enemiesList = enemiesList;

        _enemiesList.Removed += OnEnemyKilled;
    }

    private void Start()
    {
        UpdateText();
    }

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text =
            "Всего врагов = " + _enemiesList.Count.ToString() + "\n" +
            "Врагов убито = " + _killCount;

    }
    private void OnEnemyKilled(SimpleCharacter character)
    {
        _killCount++;
    }
}
