using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate float onTime();
    public event onTime OnTime;

    //public event Action OnTime;
    private float _delay;
    private float _time;
    private bool _isInitialised;

    public void Initialise(float delay) 
    {
        _delay = delay;
        _isInitialised = true;
    }

    private void FixedUpdate()
    {
        if (_isInitialised)
        {
            if (_time >= _delay)
            {
                OnTime?.Invoke();
                _time = 0;
            }
            else
            {
                _time += Time.deltaTime;
            }
        }
    }

    public void StopTimer()
    {
        _isInitialised = false;
        _time = 0;
    }

    public float GetPassedTime()
    {
        return _time;
    }

    public void UpdateTimer()
    {
        _time = 0;
    }

    public void UpdateTimer(float newDelay)
    {
        _delay = newDelay;
        _time = 0;
    }
}
