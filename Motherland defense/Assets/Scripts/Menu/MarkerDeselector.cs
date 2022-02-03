using System.Collections.Generic;
using UnityEngine;

public class MarkerDeselector : MonoBehaviour
{
    [SerializeField] private List<LevelMarker> _markers;
    [SerializeField] private GameObject _panel;

    public void Deselect()
    {
        _markers.ForEach(m => m.Deselect());
        _panel.SetActive(false);
    }
}
