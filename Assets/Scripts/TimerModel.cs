using System;
using UniRx;
using UnityEngine;

public class TimerModel
{
    // 内部タイマーのReactiveProperty
    private readonly ReactiveProperty<float> _timer;
    // 外部公開用のReadOnlyReactiveProperty
    public IReadOnlyReactiveProperty<float> Timer => _timer;
    
    // クリア回数のReactiveProperty
    private readonly ReactiveProperty<int> _clearCount;
    public IReadOnlyReactiveProperty<int> ClearCount => _clearCount;

    // コンストラクタで初期化
    public TimerModel()
    {
        _timer = new ReactiveProperty<float>(0);
        _clearCount = new ReactiveProperty<int>(0);
    }

    // タイマーをリセットする
    public void ResetTimer()
    {
        _timer.Value = InGameConst.MIN_TIMER_VALUE;
    }

    // タイマーを更新する
    public void UpdateTimer(float deltaTime, float timeScale)
    {
        if (_timer.Value > InGameConst.MAX_TIMER_VALUE)
        {
            _timer.Value = InGameConst.MIN_TIMER_VALUE;
        }

        _timer.Value += deltaTime * timeScale;
    }
    
    public void AddClearCount()
    {
        _clearCount.Value++;
    }
}