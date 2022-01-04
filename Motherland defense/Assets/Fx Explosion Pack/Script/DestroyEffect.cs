using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour 
{
	[SerializeField] private float _delayToDestroy;
    private void Start()
    {
        StartCoroutine(nameof(DestroyExplosion));   
    }

	private IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(_delayToDestroy);
        Destroy(gameObject);
    }

}
