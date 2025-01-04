using UnityEngine;
using UniRx;

public class TimerPresenter : MonoBehaviour
{
    private TimerModel _model;
    
    [SerializeField]
    private TimerView _view;
    
    private bool isStopped = false;
    
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
        _view.OnClearButtonClicked += OnClearClicked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped)
        {
            _model.UpdateTimer(Time.deltaTime, 2);
        }
    }
    
    private void OnStopButtonClicked()
    {
        isStopped = true;
        
        if(_model.Timer.Value >= 9.0f && _model.Timer.Value <= 9.9f)
        {
            _model.AddClearCount();
            _view.ShowClearPanel();
        }
        else
        {
            _model.ResetTimer();
            isStopped = false;
        }
    }

    private void OnClearClicked()
    {
        _model.ResetTimer();
        _view.HideClearPanel();
        isStopped = false;
    }
}
