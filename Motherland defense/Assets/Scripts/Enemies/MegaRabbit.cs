using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaRabbit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TowerSpot>())
        {
            other.GetComponent<TowerSpot>().DestroySpot();
        }
    }
}
