using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UI : MonoBehaviour
{
    [SerializeField] private UIPanel _towerPanel; 
    [SerializeField] private UIPanel _characterPanel;
    [SerializeField] ModelSpawner _spawner;

    private void Start()
    {
        
    }

    public void CalculateMaxValues()
    {
        var objects = _spawner.GetObjects();
        float radius = objects.Where(tower => tower.GetComponent<Tower>()).Select(rad => )
    }
}
