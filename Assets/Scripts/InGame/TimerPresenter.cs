using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using unityroom.Api;
using Cysharp.Threading.Tasks;

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
        
        isStopped = false;
        
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
        _view.OnStopButtonClicked += () => OnStopButtonClicked().Forget();
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
    
    async UniTask OnStopButtonClicked()
    {
        var token = this.GetCancellationTokenOnDestroy();
        
        isStopped = true;
        
        // クリア条件の判定はTimerModelで行っている
        if (_model.IsClearConditionMet())
        {
            AudioManager.instance_AudioManager.PlaySE(1);
            await UniTask.Delay(500, cancellationToken: token);
            
            _model.AddClearCount();
            _view.ShowClearPanel();
            timeScale += InGameConst.ADDITTIONAL_TIMESCALE;
        }
        else
        {
            AudioManager.instance_AudioManager.PlaySE(2);
            await UniTask.Delay(500, cancellationToken: token);
            _view.ShowFailedPanel();
        }
    }

    private void OnClearButtonClicked()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        _model.ResetTimer();
        _view.HideClearPanel();
        isStopped = false;
    }

    private void OnFailedButttonClicked()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        
        // 今回のスコアを保存
        PlayerPrefs.SetInt("NowScore", _model.ClearCount.Value);
        
        // ハイスコアを更新、unityroomのランキングに送信
        if (PlayerPrefs.GetInt("HighScore") < _model.ClearCount.Value)
        {
            PlayerPrefs.SetInt("HighScore", _model.ClearCount.Value);
            UnityroomApiClient.Instance.SendScore
                (1, PlayerPrefs.GetInt("HighScore"), ScoreboardWriteMode.HighScoreDesc);
        }
        
        PlayerPrefs.Save();
        SceneManager.LoadScene("Result");
    }

    private void OnRetryButttonClicked()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        
        _model.ResetTimer();
        _model.ResetClearCount();
        
        timeScale = InGameConst.DEFAULT_TIMESCALE;
        
        _view.HideFailedPanel();
        isStopped = false;
    }
}
