using TMPro;
using UnityEngine;

public class StayAliveTimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private SimpleCharacter _character;
    private bool _isTimerRunning;
    private float _startTime;

    public void Init(SimpleCharacter character)
    {
        _character = character;
    }

    private void Update()
    {
        if (_character == null || _timerText == null)
            return;

        if (_character.ShouldShowTimer == false)
        {
            Hide();
            return;
        }

        if (_isTimerRunning == false)
            StartTimer();

        UpdateTimerText();
    }

    private void StartTimer()
    {
        _startTime = Time.time;
        _isTimerRunning = true;
    }

    private void Hide()
    {
        _isTimerRunning = false;
        _timerText.text = string.Empty;
    }

    private void UpdateTimerText()
    {
        float elapsedTime = Time.time - _startTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        _timerText.text = $"Живём уже: {minutes:00}:{seconds:00}";
    }
}