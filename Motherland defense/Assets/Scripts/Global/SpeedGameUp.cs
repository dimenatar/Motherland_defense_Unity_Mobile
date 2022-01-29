using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGameUp : MonoBehaviour
{
    private bool _isClicled;

    public void ChangeSpeed()
    {
        if (_isClicled)
        {
            SetDefaultSpeed();
        }
        else
        {
            SpeedUp();
        }
    }

    private void SpeedUp()
    {
        Time.timeScale = 2;
        _isClicled = true;
    }

    private void SetDefaultSpeed()
    {
        Time.timeScale = 1;
        _isClicled = false;
    }
}
