using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisabler : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects;

    public void Disable()
    {
        foreach (var obj in _objects)
        {
            obj.SetActive(false);
        }
    }
}
