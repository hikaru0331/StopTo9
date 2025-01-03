using System;
using UniRx;

public class TimerModel
{
    private readonly ReactiveProperty<float> _timer;
    public IReadOnlyReactiveProperty<float> Timer => _timer;
    
    public TimerModel()
    {
        _timer = new ReactiveProperty<float>(0);
    }
    
    public void ResetTimer()
    {
        _timer.Value = 0;
    }
    
    public void UpdateTimer(float deltaTime)
    {
        _timer.Value += deltaTime;
        
        if (_timer.Value >= 10)
        {
            _timer.Value = 0;
        }
    }
}
