using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ting : Tower
{
    public override IEnumerator Shoot()
    {
        while (true)
        {
            if (GetTarget())
            {
                CreateArrow();
                PlayShotSound();
            }
            yield return new WaitForSeconds(GetReloadTime());
        }
    }

    private void Start()
    {
        StartCoroutine(nameof(Shoot));
    }

    private void CreateArrow()
    {
        SetUpArrow(Instantiate(Resources.Load<GameObject>("ArrowPrefab"), GetShotStartPosition().position, Quaternion.Euler(0f,GetTransform(), 0f)));
    }

    private void SetUpArrow(GameObject arrow)
    {
        arrow.GetComponent<Arrow>().SetTarget(GetTarget(), Data.Damage);
        arrow.SetActive(true);
    }
}
