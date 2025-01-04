using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class TimerPresenter : MonoBehaviour
{
    private TimerModel _model;
    
    [SerializeField]
    private TimerView _view;
    
    private bool isStopped = false;

    private float timeScale = InGameConst.DEFAULT_TIMESCALE;
    
    void Start()
    {
        _model = new TimerModel();
        _view.Initialize();
        
        Bind();
        SetEvents();
    }
    
    private void Bind()
    {
        // ModelのTimingの値が変わった際に、Viewを更新する
        _model.Timer
            .Subscribe(_ => _view.SetTimerText(_model.GetTimerDisplayValue())) // タイマー表示用メソッドを使用
            .AddTo(gameObject);
        
        // ModelのClearCountの値が変わった際に、Viewを更新する
        _model.ClearCount
            .Subscribe(_view.SetClearCountText)
            .AddTo(gameObject);
    }
    
    private void SetEvents()
    {
        _view.OnStopButtonClicked += OnStopButtonClicked;
        _view.OnClearButtonClicked += OnClearButtonClicked;
        _view.OnFailedButtonClicked += OnFailedButttonClicked;
        _view.OnRetryButtonClicked += OnRetryButttonClicked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped)
        {
            _model.UpdateTimer(Time.deltaTime, timeScale);
        }
    }
    
    private void OnStopButtonClicked()
    {
        isStopped = true;
        
        // クリア条件の判定はTimerModelで行っている
        if (_model.IsClearConditionMet())
        {
            _model.AddClearCount();
            _view.ShowClearPanel();
            timeScale += InGameConst.ADDITTIONAL_TIMESCALE;
        }
        else
        {
            _view.ShowFailedPanel();
        }
    }

    private void OnClearButtonClicked()
    {
        _model.ResetTimer();
        _view.HideClearPanel();
        isStopped = false;
    }

    private void OnFailedButttonClicked()
    {
        PlayerPrefs.SetInt("NowScore", _model.ClearCount.Value);
        PlayerPrefs.Save();
        _model.ResetClearCount();
        SceneManager.LoadScene("Result");
    }

    private void OnRetryButttonClicked()
    {
        _model.ResetTimer();
        _model.ResetClearCount();
        
        timeScale = InGameConst.DEFAULT_TIMESCALE;
        
        _view.HideFailedPanel();
        isStopped = false;
    }
}
