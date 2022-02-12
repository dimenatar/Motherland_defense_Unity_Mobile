using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class EnemyWaves : MonoBehaviour
{
    [SerializeField] private StartWaves _startWave;
    [SerializeField] private List<EnemyWave> _enemyWaves;
    [SerializeField] private float _waveActivatePause;
    private int _waveIndex = -1;
    private Timer _timer;

    public List<EnemyWave> Waves => _enemyWaves;

    public float StartOffense()
    {
        _startWave.OnStartWave += StartNewWave;
        _timer.Initialise(_waveActivatePause);
        StartNewWave();
        return _waveActivatePause;
    }

    public void EndOffense()
    {
        _startWave.EndOffense();
    }

    //������ �����
    private void Start()
    {
        _timer = GetComponent<Timer>();
        _timer.OnTime += StartNewWave;
        _startWave.OnStartWave += StartOffense;
    }

    private float StartNewWave()
    {
        _waveIndex++;
        if (_waveIndex >= _enemyWaves.Count)
        {
            EndOffense();
            return 0;
        }
        else
        {
            _enemyWaves[_waveIndex].StartWave();
            return _enemyWaves[_waveIndex].WavePause;
        }
    }
}
