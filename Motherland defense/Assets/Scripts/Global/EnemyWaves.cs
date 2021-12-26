using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    [SerializeField] private List<EnemyWave> _enemyWaves;
    [SerializeField] private float _waveActivatePause;
    private int _waveIndex = -1;

    public void StartOffense()
    {
        StartCoroutine(nameof(StartNewWave));
    }
    public void EndOffense()
    {
        StopCoroutine(nameof(StartNewWave));
    }
    //убрать потом
    private void Start()
    {
        StartOffense();
    }
    private IEnumerator StartNewWave()
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
        yield return new WaitForSeconds(_waveActivatePause);
    }
}
