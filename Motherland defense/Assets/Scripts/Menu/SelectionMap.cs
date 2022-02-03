using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMap : MonoBehaviour
{
    [SerializeField] private MarkerDeselector _markerDeselector;

    public void Press()
    {
        _markerDeselector.Deselect();
    }
}
