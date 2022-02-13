using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWaves : MonoBehaviour, IClickable
{
    [SerializeField] Image _startWaveImage;
    [SerializeField] Timer _timer;

    public delegate float StartOffense();
    public event StartOffense OnStartWave;
    public event Action OnEndOffense;

    private float? _timeToFill;
    private bool isStarted;
    public void Deselect() {}

    public void ObjectClick()
    {
        if (!isStarted)
        {
            _timeToFill = OnStartWave?.Invoke();
            if (_timeToFill != null && _timeToFill != -1)
            {
                _timer.Initialise(_timeToFill.Value);
            }
            else if (_timeToFill == -1)
            {
                StopCircle();
                return;
            }
            isStarted = true;
            StartCoroutine(nameof(FillCircle));
        }
    }

    public void EndOffense()
    {
        _startWaveImage.fillAmount = 0;
        StopCoroutine(nameof(FillCircle));
    }

    private void StopCircle()
    {
        StopCoroutine(nameof(FillCircle));
        _startWaveImage.fillAmount = 0;
    }

    private float RestartCircle()
    {
        _startWaveImage.fillAmount = 0;
        return 0;
    }

    private void Start()
    {
        _timer.OnTime += RestartCircle;
    }

    private IEnumerator FillCircle()
    {
        while (true)
        {
            _startWaveImage.fillAmount = _timer.GetPassedTime() / _timeToFill.Value;
            yield return new WaitForFixedUpdate();
        }
    }
}
