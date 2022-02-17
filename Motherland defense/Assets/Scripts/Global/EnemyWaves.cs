using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class EnemyWaves : MonoBehaviour
{
    public event Action OnStartNewWave;

    [SerializeField] private List<EnemyWave> _enemyWaves;
    [SerializeField] private List<WaveIcon> _startOffenseIcons;
    private int _waveIndex = -1;
    private Timer _timer;
    private bool _isStartedOffense;
    public List<EnemyWave> Waves => _enemyWaves;

    //убрать потом
    private void Start()
    {
        _timer = GetComponent<Timer>();
        _timer.OnTime += StartNewWave;
        _enemyWaves.ForEach(wave => wave.OnStartedWave += DisableCurrentWaveIcon);
        _enemyWaves.ForEach(wave => wave.OnStartedWave += EnableNextWaveIcon);
        _startOffenseIcons.ForEach(icon => icon.OnStartWave += StartOffense);
    }

    private float StartOffense()
    {
        if (!_isStartedOffense)
        {
            _isStartedOffense = true;
            _timer.Initialise(_enemyWaves[0].WavePause);
            StartNewWave();
            return _enemyWaves[0].WavePause;
        }
        else
        {
            return -1;
        }
    }

    private void EnableNextWaveIcon(EnemyWave current)
    {
        int currentIndex = _enemyWaves.FindIndex(wave => wave == current);
        if (currentIndex < _enemyWaves.Count-1)
        {
          
            _enemyWaves[currentIndex + 1].EnableIcon(_enemyWaves[currentIndex].WavePause);
        }
    }

    private void DisableCurrentWaveIcon(EnemyWave current)
    {
        int currentIndex = _enemyWaves.FindIndex(wave => wave == current);
        _enemyWaves[currentIndex].DisableIcon();
    }

    private float StartNewWave()
    {
        if (_waveIndex >= _enemyWaves.Count-1)
        {
            StopWaves();
            return 0;
        }
        else
        {
            OnStartNewWave?.Invoke();
            _waveIndex++;
            _enemyWaves[_waveIndex].StartWave();
            _timer.Initialise(_enemyWaves[_waveIndex].WavePause);
            return _enemyWaves[_waveIndex].WavePause;
        }
    }

    private void StopWaves()
    {
        _timer.StopTimer();
    }
}
