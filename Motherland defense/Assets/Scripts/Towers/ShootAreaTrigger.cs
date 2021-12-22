using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAreaTrigger : MonoBehaviour
{
    //[SerializeField] private ITower _tower;
    [SerializeField] private ITower _tower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            Debug.Log("pidor zdes");
            _tower.AddEnemyInArea(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            _tower.RemoveEnemyInArea(other.gameObject);
        }
    }
    private void Start()
    {
        _tower = transform.parent.GetComponent<ITower>();
    }
}
