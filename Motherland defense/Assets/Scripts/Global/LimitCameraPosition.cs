using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitCameraPosition : MonoBehaviour
{
    [SerializeField] private Transform _limimBox;
    [SerializeField] private Transform _camera;

    void Update()
    {

        var x = StayInRange(0.5f, -0.5f, _camera.localPosition.x);
        var z = StayInRange(0.5f, -0.5f, _camera.localPosition.z);
        _camera.localPosition = new Vector3(x, _camera.localPosition.y, z);
    }

    private float StayInRange(float maxValue, float minValue, float position)
    {
        if (position > maxValue) return maxValue;
        if (position < minValue) return minValue;
        return position;
    }
}
