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

    public float StartOffense()
    {
        _startWave.OnStartOffense -= StartOffense;
        _timer.Initialise(_waveActivatePause);
        StartNewWave();
        return _waveActivatePause;
    }
    public void EndOffense()
    {
        
    }
    //убрать потом
    private void Start()
    {
        _timer = GetComponent<Timer>();
        _timer.OnTime += StartNewWave;
        _startWave.OnStartOffense += StartOffense;
        //StartOffense();
    }


    private void StartNewWave()
    {
        _waveIndex++;
        if (_waveIndex >= _enemyWaves.Count)
        {
            EndOffense();
        }
        else
        {
            _enemyWaves[_waveIndex].StartWave();
        }
    }
}
