using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    // �������� ��� �����
    public GameObject Opponent = null;

    public IEnumerator FightWithOpponent()
    {
        while (Opponent)
        {
            //�� ��������� ���
            yield return new WaitForSeconds(0.5f);
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
