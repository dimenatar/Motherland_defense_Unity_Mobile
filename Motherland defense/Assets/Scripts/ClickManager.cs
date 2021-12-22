using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickOnObject();
        }
    }
    private void ClickOnObject()
    {
        
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            RaycastHit[] rays = Physics.RaycastAll(ray);
            FilterRaycast(ref rays);
            if (rays.Length > 0)
            {
                IClickable clickable = rays[0].collider.GetComponent<IClickable>();
                clickable.OnObjectClick();
            }
        }
    }
    private void FilterRaycast(ref RaycastHit[] raycasts)
    {
        List<RaycastHit> raycastList = raycasts.ToList();
        raycastList.RemoveAll(element => element.collider.GetComponent<IClickable>() == null);
        raycasts = raycastList.ToArray();
    }
}
