using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTimer : MonoBehaviour
{
    [SerializeField]
    private float _destroyTime;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
    }
    public void ManageTimer()
    {
        _timer += Time.deltaTime;
        if (_timer >= _destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}
