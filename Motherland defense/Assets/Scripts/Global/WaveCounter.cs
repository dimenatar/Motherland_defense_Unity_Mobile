using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounter : MonoBehaviour
{
    [SerializeField] private EnemyWaves _enemyWaves;
    [SerializeField] private Text _currentWaveText;
    [SerializeField] private Text _amountWaveText;
    private int waveCount;

    private void Start()
    {
        _enemyWaves.OnStartNewWave += IncrementWaveCount;
        SetTotalAmountWaves();
    }

    private void SetTotalAmountWaves()
    {
        _amountWaveText.text = _enemyWaves.Waves.Count.ToString();
    }

    private void IncrementWaveCount()
    {
        waveCount++;
        _currentWaveText.text = waveCount.ToString();
    }
}
