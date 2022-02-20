using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBodyAfterTime : MonoBehaviour
{
    private const float _timeToDestroy = 12;

    private void Start()
    {
        Invoke(nameof(DestroyBody), _timeToDestroy);
    }

    private void DestroyBody()
    {
        Destroy(gameObject);
    }
}
