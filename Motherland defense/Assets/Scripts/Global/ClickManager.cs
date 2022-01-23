using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

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
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            RaycastHit[] rays = Physics.RaycastAll(ray);
            FilterRaycast(ref rays);
            if (rays.Length > 0)
            { 
                IClickable clickable = rays[0].collider.GetComponent<IClickable>();
                clickedObject = clickable;
                Debug.Log(rays[0].collider.gameObject.name);
                clickable.ObjectClick();
            }

        }
        if (clickedObject != null)
        {
            OnObjectClick -= clickedObject.Deselect;
        }

        OnObjectClick?.Invoke();
        if (clickedObject != null)
        {
            OnObjectClick += clickedObject.Deselect;
        }
        
        clickedObject = null;
    }
    private void FilterRaycast(ref RaycastHit[] raycasts)
    {
        List<RaycastHit> raycastList = raycasts.ToList();
        raycastList.RemoveAll(element => element.collider.GetComponent<IClickable>() == null);
        raycasts = raycastList.ToArray();
    }
}
