using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class TimerPresenter : MonoBehaviour
{
    private TimerModel _model;
    
    [SerializeField]
    private TimerView _view;
    
    private bool isStopped = false;

    private float timeScale = 1.0f;
    
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
            .Subscribe(_view.SetTimerText)
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
        
        if(_model.Timer.Value >= InGameConst.MIN_CLEAR_TIME && _model.Timer.Value <= InGameConst.MAX_CLEAR_TIME)
        {
            _model.AddClearCount();
            _view.ShowClearPanel();
            timeScale += 0.5f;
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
        PlayerPrefs.SetInt("HighScore", _model.ClearCount.Value);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Result");
    }
}
