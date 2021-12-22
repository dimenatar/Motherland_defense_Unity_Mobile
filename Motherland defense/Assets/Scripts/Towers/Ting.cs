using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ting : Tower
{
    private Vector3 diff;
    private float rotateZ;
    public override IEnumerator Shoot()
    {
        while (true)
        {
            if (GetTarget())
            {
                CreateArrow();
            }
            yield return new WaitForSeconds(GetReloadTime());
        }

    }
    public float GetTransform()
    {
        diff = GetTarget().transform.position - GetShotStartPosition().position;
        rotateZ = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
        return rotateZ;
    }
    private void Start()
    {
        StartCoroutine(nameof(Shoot));
    }
    private void CreateArrow()
    {
        Debug.DrawLine(GetShotStartPosition().position, GetTarget().transform.position - GetShotStartPosition().position);
        SetUpArrow(Instantiate(Resources.Load<GameObject>("ArrowPrefab"), GetShotStartPosition().position, Quaternion.Euler(0f,GetTransform(), 0f)));

    }
    private void SetUpArrow(GameObject arrow)
    {
        //arrow.transform.LookAt(GetTarget().transform.position+new Vector3(0,35,0));
        arrow.GetComponent<Arrow>().SetTarget(GetTarget());
        arrow.SetActive(true);
    }
}
