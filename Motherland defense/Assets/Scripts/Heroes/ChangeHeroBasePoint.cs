using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHeroBasePoint : MonoBehaviour
{
    [SerializeField] private GameObject _heroButton;
    [SerializeField] private TowerSpot _spot;
    [SerializeField] private TowerData _gymData;

    private bool _allowToChange;
    private HeroMove _heroMove;

    private void Update()
    {
        if (_allowToChange)
        {
            ManageClickedPoint();
        }
    }

    public void EnableChangePointButton(Hero hero)
    {
        _heroButton.SetActive(true);
        _heroMove = hero.GetComponent<HeroMove>();
    }

    public void DisableChangePointButton()
    {
        _heroButton.SetActive(false);

    }

    public void AllowToChange()
    {
        Debug.Log("Allow");
        _spot.PreventDeselect();
        _allowToChange = true;
    }

    private void ManageClickedPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointInRadius(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                _heroMove.ChangeDefaultPoint(GetRayHitPoint(Input.mousePosition));
                _allowToChange = false;
            }
            else
            {
                Debug.LogWarning("value out of range");
            }
            _spot.AllowDeselect();
            _spot.Deselect();
        }
    }

    private bool IsPointInRadius(Vector3 clickPoint)
    {
        Vector3 distanceFromGym = transform.position - GetRayHitPoint(clickPoint);
        Debug.Log(distanceFromGym);
        Debug.Log(_gymData.Radius);
        return Mathf.Abs(distanceFromGym.x) <= _gymData.Radius*5 && Mathf.Abs(distanceFromGym.z) <= _gymData.Radius*5;
    }

    private Vector3 GetRayHitPoint(Vector3 point)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (_spot.Collider.Raycast(ray, out RaycastHit hitData, 4000))
        {
            return hitData.point;
        }
        return point;
    }
}
