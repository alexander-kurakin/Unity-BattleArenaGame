using TMPro;
using UnityEngine;

public class StayAliveTimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private StayAliveTimer _timer;

    public void Init(StayAliveTimer timer)
    {
        _timer = timer;
    }

    private void Update()
    {
        if (_timerText == null || _timer == null)
            return;

        UpdateTimerText();
    }

    public void Hide()
    {
        _timer = null;
        _timerText.text = string.Empty;
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(_timer.ElapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_timer.ElapsedTime % 60f);

        _timerText.text = $"Живём уже: {minutes:00}:{seconds:00}";
    }
}