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

    /// <summary>
    /// タイマーをリセットするメソッド
    /// </summary>
    public void ResetTimer()
    {
        _timer.Value = InGameConst.MIN_TIMER_VALUE;
    }

    /// <summary>
    /// タイマーを更新するメソッド。範囲外の整数値も制限している
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <param name="timeScale"></param>
    public void UpdateTimer(float deltaTime, float timeScale)
    {
        if (_timer.Value >= InGameConst.MAX_TIMER_VALUE)
        {
            _timer.Value = InGameConst.MIN_TIMER_VALUE;
        }

        _timer.Value += deltaTime * timeScale;
        
        // 範囲外の値を制限
        _timer.Value = Mathf.Clamp(_timer.Value, InGameConst.MIN_TIMER_VALUE, InGameConst.MAX_TIMER_VALUE);
    }
    
    /// <summary>
    /// 表示用のタイマー値を取得するメソッド (少数を切り捨てた整数値)
    /// </summary>
    public int GetTimerDisplayValue()
    {
        return Mathf.FloorToInt(_timer.Value);
    }

    /// <summary>
    /// クリア条件を満たしているか判定
    /// </summary>
    public bool IsClearConditionMet()
    {
        // 表示上の数値ではなく内部値で判定
        return _timer.Value >= InGameConst.MIN_CLEAR_TIME && _timer.Value <= InGameConst.MAX_CLEAR_TIME;
    }
    
    public void AddClearCount()
    {
        _clearCount.Value++;
    }

    public void ResetClearCount()
    {
        _clearCount.Value = 0;
    }
}