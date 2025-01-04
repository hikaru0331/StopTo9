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
    private TextMeshProUGUI timerText;
    
    [SerializeField]
    private TextMeshProUGUI clearCountText;

    [SerializeField]
    private GameObject clearPanel;
    [SerializeField]
    private Button clearButton;
    public Action OnClearButtonClicked;
    
    public void Initialize()
    {
        stopButton.onClick.AddListener(OnStopButtonClickedEvent);

        clearButton.onClick.AddListener(OnClearButtonClickedEvent);
        clearPanel.SetActive(false);
    }

    public void OnStopButtonClickedEvent()
    {
        OnStopButtonClicked?.Invoke();
    }
    
    public void OnClearButtonClickedEvent()
    {
        OnClearButtonClicked?.Invoke();
    }
    
    public void SetTimerText(float time)
    {
        timerText.text = time.ToString("F0");
    }

    public void SetClearCountText(int count)
    {
        clearCountText.text = count.ToString();
    }
    
    public void ShowClearPanel()
    {
        clearPanel.SetActive(true);
    }
    
    public void HideClearPanel()
    {
        clearPanel.SetActive(false);
    }
}
