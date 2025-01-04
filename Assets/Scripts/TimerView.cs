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

    [SerializeField] 
    private GameObject failedPanel;
    [SerializeField]
    private Button failedButton;
    public Action OnFailedButtonClicked;
    [SerializeField]
    private Button retryButton;
    public Action OnRetryButtonClicked;
    
    // 初期化処理
    public void Initialize()
    {
        stopButton.onClick.AddListener(OnStopButtonClickedEvent);

        clearButton.onClick.AddListener(OnClearButtonClickedEvent);
        clearPanel.SetActive(false);
        
        failedButton.onClick.AddListener(OnFailedButtonClickedEvent);
        retryButton.onClick.AddListener(OnRetryButtonClickedEvent);
        failedPanel.SetActive(false);
    }

    // イベントの設定
    public void OnStopButtonClickedEvent()
    {
        OnStopButtonClicked?.Invoke();
    }
    
    public void OnClearButtonClickedEvent()
    {
        OnClearButtonClicked?.Invoke();
    }

    public void OnFailedButtonClickedEvent()
    {
        OnFailedButtonClicked?.Invoke();
    }

    public void OnRetryButtonClickedEvent()
    {
        OnRetryButtonClicked?.Invoke();
    }
    
    /// <summary>
    /// タイマーの表示処理
    /// </summary>
    /// <param name="time"></param>
    public void SetTimerText(float time)
    {
        timerText.text = time.ToString("F0");
    }

    /// <summary>
    /// クリア回数の表示処理
    /// </summary>
    /// <param name="count"></param>
    public void SetClearCountText(int count)
    {
        clearCountText.text = count.ToString("D3");
    }
    
    // クリア時のパネルの表示・非表示
    public void ShowClearPanel()
    {
        clearPanel.SetActive(true);
    }
    
    public void HideClearPanel()
    {
        clearPanel.SetActive(false);
    }
    
    // 失敗時のパネルの表示・非表示
    public void ShowFailedPanel()
    {
        failedPanel.SetActive(true);
    }
    
    public void HideFailedPanel()
    {
        failedPanel.SetActive(false);
    }
}
