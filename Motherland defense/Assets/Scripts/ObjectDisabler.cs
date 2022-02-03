using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisabler : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects;
    private bool _canBeEnabled = true;

    public void Disable()
    {
        foreach (var obj in _objects)
        {
            obj.SetActive(false);
        }
    }

    public void Enable()
    {
        if (_canBeEnabled)
        {
            foreach (var item in _objects)
            {
                item.SetActive(true);
            }
        }
    }

    public void DisableForever()
    {
        _canBeEnabled = false;
        foreach (var obj in _objects)
        {
            obj.SetActive(false);
        }
    }
}
