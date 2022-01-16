using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _cameraTransform;
    void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (_cameraTransform != null)
        {
            transform.LookAt(transform.position + _cameraTransform.forward);
        }
        else
        {
            _cameraTransform = Camera.main.transform;
            Debug.Log("null");
        }
    }
}