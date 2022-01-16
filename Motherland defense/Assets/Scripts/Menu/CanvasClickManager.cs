using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasClickManager : MonoBehaviour
{
    private PointerEventData _data;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ManageClick();
        }
    }
    private void Start()
    {
        
    }
    private void OnGUI()
    {
        
    }

    private void ManageClick()
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current), results);
        Debug.Log(results.Count);
        results.ForEach(r=> Debug.Log(r.gameObject.name));
        //foreach (var r in results)
        //{
        //    Debug.Log(r.gameObject.name);
        //}
    }    
}
