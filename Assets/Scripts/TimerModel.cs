using System;
using UniRx;

public class TimerModel
{
    // 内部タイマーのReactiveProperty
    private readonly ReactiveProperty<float> _timer;

    // 外部公開用のReadOnlyReactiveProperty
    public IReadOnlyReactiveProperty<float> Timer => _timer;

    // タイマーが設定されたときのイベント
    public event Action<float> OnUpdateTimer;

    // コンストラクタで初期化
    public TimerModel()
    {
        _timer = new ReactiveProperty<float>(0);
    }

    // タイマーをリセットする
    public void ResetTimer()
    {
        _timer.Value = 0;
    }

    // タイマーを更新する
    public void UpdateTimer(float deltaTime)
    {
        _timer.Value += deltaTime;

        if (_timer.Value >= 10)
        {
            _timer.Value = 0;
        }
        
        OnUpdateTimer?.Invoke(_timer.Value);
    }
}