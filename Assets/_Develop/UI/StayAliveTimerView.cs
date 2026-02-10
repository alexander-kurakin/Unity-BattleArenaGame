using TMPro;
using UnityEngine;

public class StayAliveTimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private SimpleCharacter _character;
    private StayAliveTimer _timer;

    public void Init(SimpleCharacter character, StayAliveTimer timer)
    {
        _character = character;
        _timer = timer;
    }

    private void Update()
    {
        if (_character == null || _timerText == null || _timer == null)
            return;

        if (_character.ShouldShowTimer == false)
        {
            Hide();
            return;
        }

        UpdateTimerText();
    }

    private void Hide()
    {
        _timerText.text = string.Empty;
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(_timer.ElapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_timer.ElapsedTime % 60f);

        _timerText.text = $"Живём уже: {minutes:00}:{seconds:00}";
    }
}