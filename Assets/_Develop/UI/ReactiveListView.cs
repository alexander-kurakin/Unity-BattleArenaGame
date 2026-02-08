using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReactiveListView : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    private ReactiveList<SimpleCharacter> _enemiesList = new ReactiveList<SimpleCharacter>();

    private void Init(ReactiveList<SimpleCharacter> enemiesList)
    {
        _enemiesList = enemiesList;
    }

    private void Start()
    {
        _text.text =
            "Всего врагов = " + _enemiesList.Count.ToString() + "\n" +
            "Врагов убито = " + _levelConfig.EnemySpawnRadius.ToString() + "\n"
    }
}
