using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    [SerializeField] private GameObject _limitBox;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _scaleForce;
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _minHeight;
    [SerializeField] private float _smoothness;
    private Touch _touch1;
    private Touch _touch2;
    private Vector2 _startDifferense = Vector2.zero;
    private Vector2 _difference;
    private int _dirrection = 1;

    private void FixedUpdate()
    {
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(0).position.x < Input.GetTouch(1).position.x)
            {
                _touch1 = Input.GetTouch(1);
                _touch2 = Input.GetTouch(0);
            }
            else
            {
                _touch1 = Input.GetTouch(0);
                _touch2 = Input.GetTouch(1);
            }
            if (_startDifferense != Vector2.zero)
            {
                _difference = _touch2.position - _touch1.position - _startDifferense;
                if (_difference.x > 0)
                {
                    _dirrection = 1;
                }
                else
                {
                    _dirrection = -1;
                }
                if (CheckLimits())
                {
                    Debug.Log($"difference = {_difference}");
                    Debug.Log($"Start localPos ={ _camera.localPosition}");
                    //_camera.localPosition += new Vector3(0, _difference.magnitude * _scaleForce, 0);
                    _camera.localPosition = Vector3.Lerp(_camera.localPosition, _camera.localPosition + new Vector3(0, _difference.magnitude, 0) * _dirrection * _scaleForce, _smoothness * Time.deltaTime);
                    _limitBox.transform.localScale = Vector3.Lerp(_limitBox.transform.localScale, _limitBox.transform.localScale - new Vector3(_difference.magnitude, 0, _difference.magnitude)*_dirrection*_scaleForce, _smoothness*Time.deltaTime);
                }
                else
                {
                    Debug.LogError("borders");
                }
            }
            _startDifferense = _touch2.position - _touch1.position;
        }
        else
        {
            _startDifferense = Vector2.zero;
        }
    }

    private bool CheckLimits()
    {
        if (_difference.x > 0 && Vector3.Lerp(_camera.localPosition, _camera.localPosition + new Vector3(0, _difference.magnitude, 0) * _dirrection * _scaleForce, _smoothness * Time.deltaTime).y < _maxHeight)
        {
            return true;
        }
        else if (_difference.x <= 0 && Vector3.Lerp(_camera.localPosition, _camera.localPosition + new Vector3(0, _difference.magnitude, 0) * _dirrection * _scaleForce, _smoothness * Time.deltaTime).y > _minHeight)
        {
            return true;
        }
        return false;
    }
}
