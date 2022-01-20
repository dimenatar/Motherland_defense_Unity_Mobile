using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    private GameObject _rotatableObject;
    private bool _isHolding = false;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private Vector2 _difference;
    private float _rotateSpeed;
    private float _slowerRotationForce;

    public void Initialise(GameObject obj)
    {
        _rotatableObject = obj;
    }
    private void Start()
    {
        _rotatableObject = gameObject;    
    }

    public void SetRotationSpeed(float speed, float slowerRotationForce)
    {
        _rotateSpeed = speed;
        _slowerRotationForce = slowerRotationForce;
    }

    private void Rotate()
    {
        if (_rotatableObject != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isHolding = true;
                _startPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                _startPosition = Input.mousePosition;
                if (_endPosition != Vector2.zero && _isHolding)
                {
                    _difference = (_endPosition - _startPosition) * _rotateSpeed;
                    transform.Rotate(0, _difference.x, 0);
                    //transform.rotation = Quaternion.Euler(0, _difference.x,0);
                }
                _endPosition = _startPosition;
            }
            else
            {
                if (_difference.x > 0)
                {
                    _difference -= new Vector2(Time.deltaTime*_slowerRotationForce, 0);
                    transform.Rotate(0, _difference.x, 0);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isHolding = false;
                _startPosition = Vector2.zero;
                _endPosition = Vector2.zero;
            }
        }

    }

    private void FixedUpdate()
    {
        Rotate();
    }
}
