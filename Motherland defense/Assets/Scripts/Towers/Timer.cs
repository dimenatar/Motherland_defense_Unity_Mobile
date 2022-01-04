using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{

    public delegate void Time(float delay);
    public event Time OnTime;
    
    public IEnumerator WaitTime(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
