using TMPro;
using UnityEngine;

public class ReactiveListView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private IReadOnlyReactiveList<SimpleCharacter> _enemiesList;
    private int _killCount = 0;
    private bool _isInit;

    public void Init(IReadOnlyReactiveList<SimpleCharacter> enemiesList)
    {
        _enemiesList = enemiesList;

        _enemiesList.Removed += OnEnemyKilled;
        _enemiesList.Cleared += OnEnemyListCleared;

        _isInit = true;
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
        if (_isInit)
            _text.text =
            "Всего врагов = " + _enemiesList.Count.ToString() + "\n" +
            "Врагов убито = " + _killCount;

    }
    private void OnEnemyKilled(SimpleCharacter character)
    {
        _killCount++;
    }

    private void OnEnemyListCleared()
    {
        _killCount = 0;
    }
}
