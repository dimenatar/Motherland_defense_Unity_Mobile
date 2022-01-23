using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : Tower
{
    private GameObject _ball;
    public override IEnumerator Shoot()
    {
        while (true)
        {
            if (GetTarget())
            {
                PlayShotSound();
                SetUpBall();
                yield return new WaitForSeconds(GetReloadTime());
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void Start()
    {
        _ball = transform.Find("CanonBall").gameObject;
        StartCoroutine(nameof(Shoot));
    }
    private void SetUpBall()
    {
        _ball.SetActive(true);
        _ball.GetComponent<CanonBall>().ReturnToStartPoint(GetShotStartPosition().position);
        _ball.GetComponent<CanonBall>().SetTarget(GetTarget());
        _ball.GetComponent<CanonBall>().FireBall();
    }
}
