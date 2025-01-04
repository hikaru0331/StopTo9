using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField]
    private Button stopButton;
    public Action OnStopButtonClicked;
    
    [SerializeField]
    private TextMeshPro timerText;

    public void Initialize()
    {
        stopButton.onClick.AddListener(OnStopButtonClickedEvent);
    }

    public void OnStopButtonClickedEvent()
    {
        OnStopButtonClicked?.Invoke();
    }
    
    public void SetTimerText(float time)
    {
        timerText.text = time.ToString("F1");
    }
}
