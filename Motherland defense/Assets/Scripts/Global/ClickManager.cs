using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public delegate void ObjectClick();
    public event ObjectClick OnObjectClick;

    [SerializeField] private Camera _camera;
    private IClickable clickedObject = null;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ClickOnScreen();
        }
    }
    private void ClickOnScreen()
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
                clickedObject = clickable;
                clickable.ObjectClick();
            }

        }
        if (clickedObject != null)
        {
            OnObjectClick -= clickedObject.Deselect;
        }
        OnObjectClick?.Invoke();
        clickedObject = null;
    }
    private void FilterRaycast(ref RaycastHit[] raycasts)
    {
        List<RaycastHit> raycastList = raycasts.ToList();
        raycastList.RemoveAll(element => element.collider.GetComponent<IClickable>() == null);
        raycasts = raycastList.ToArray();
    }
}
