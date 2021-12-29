using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : Tower
{
    [SerializeField] private GameObject _freezeCapsule;
    [Range(0,100)] [Header("Procentage")]
    [SerializeField] private float _freezeForce;
    [SerializeField] private float _slowDownDuration;
    private FreezeBall _freezeBall;
    public override IEnumerator Shoot()
    {
        while (true)
        {
            if (GetTarget())
            {
                CreateFreezeBall();
            }
            yield return new WaitForSeconds(GetReloadTime());
            
        }

    }

    public void CreateFreezeBall()
    {
        _freezeCapsule.transform.rotation = Quaternion.Euler(0f, GetTransform(), 0f);
        _freezeBall.SetFreezeForce(_freezeForce);
        _freezeBall.SetSlowDownDuration(_slowDownDuration);
        _freezeBall.Fire();
    }

    private void Start()
    {
        StartCoroutine(nameof(Shoot));
        _freezeBall = _freezeCapsule.transform.Find("FreezeCapsule").GetComponent<FreezeBall>();
    }
}
