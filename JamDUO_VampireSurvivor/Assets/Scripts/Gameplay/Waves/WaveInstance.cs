using UnityEngine;

/// <summary>
/// Instance of a wave
/// keeps the wave alive while there's still enemies to spawn
/// </summary>
public class WaveInstance
{
    public bool IsDone => _currentOccurence >= _data.TimesToRepeat;
    
    WaveData _data;
    float _timer;
    int _currentOccurence;

    public WaveInstance(WaveData data)
    {
        _data = data;
    }

    public void Update( WavesManager manager, float currentTimer)
    {
        if (_data.TimeToStart > currentTimer  )
            return;

        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _currentOccurence++;
            _timer = _data.RepeatTimer;

            manager.Spawn(_data);
        }

    }
}
