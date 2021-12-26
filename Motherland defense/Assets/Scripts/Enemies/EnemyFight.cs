using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    // изменить тип потом
    public GameObject Opponent = null;

    [SerializeField] private float _pauseBetweenAttacks;
    public IEnumerator FightWithOpponent()
    {
        while (Opponent)
        {
            //Го пиздиться лох
            yield return new WaitForSeconds(_pauseBetweenAttacks);
        }
    }

    public void StartFight(GameObject _opponent)
    {
        Opponent = _opponent;
        StartCoroutine(nameof(FightWithOpponent));
    }

    public void StopFight()
    {
        Opponent = null;
        StopCoroutine(nameof(FightWithOpponent));
    }
}
