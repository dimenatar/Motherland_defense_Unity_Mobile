using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveIcon : MonoBehaviour, IClickable
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
                StopFilling();
                return;
            }
            isStarted = true;
            StartCoroutine(nameof(FillCircle));
        }
    }

    public void StartFilling(float seconds)
    { 
        _timeToFill = seconds;
        _timer.Initialise(_timeToFill.Value);
        StartCoroutine(nameof(FillCircle));
    }

    public void StopFilling()
    {
        StopCoroutine(nameof(FillCircle));
        _startWaveImage.fillAmount = 0;
    }

    private IEnumerator FillCircle()
    {
        while (true)
        {
            _startWaveImage.fillAmount = _timer.GetPassedTime() / _timeToFill.Value;
            if (_startWaveImage.fillAmount == 1)
            {
                StopFilling();
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
