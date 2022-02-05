using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : MonoBehaviour
{
    private EnemyMove _enemyMove;
    private EnemyAnimation _enemyAnimation;
    private float _slowDownDuraton;
    private float _startSpeed;
    private float _startAnimationSpeed;
    private float _percentage;
    private float _timer;

    public void UpdateTimer()
    {
        _timer = 0;
    }
    
    public void SlowEnemy(float percentage)
    {
        SaveValues();
        SetNewValues(percentage);
        StartCoroutine(nameof(RemoveSlowDown));
    }

    private void SetNewValues(float percentage)
    {
        _enemyMove.SetSpeed(_enemyMove.GetSpeed() * (1 - (percentage / 100)));
        _enemyAnimation.ChangeAnimationSpeed(_startAnimationSpeed * (1 - (percentage / 100)));
    }

    private void SaveValues()
    {
        _enemyMove = GetComponent<EnemyMove>();
        _enemyAnimation = GetComponent<EnemyAnimation>();
        _startAnimationSpeed = _enemyAnimation.AnimationSpeed;
        _startSpeed = _enemyMove.GetSpeed();
    }

    public void SetSlowDownDuration(float slowDownDuration)
    {
        _slowDownDuraton = slowDownDuration;
    }

    private IEnumerator RemoveSlowDown()
    {
        while (true)
        {
            if (_timer >= _slowDownDuraton)
            {
                ReturnDefaultValuesToEnemy();
                Destroy(this.GetComponent<Slower>());
            }
            else
            {
                _timer += Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();

        }
    }

    private void ReturnDefaultValuesToEnemy()
    {
        _enemyMove.SetSpeed(_startSpeed);
        _enemyAnimation.ChangeAnimationSpeed(_startAnimationSpeed);
    }
}
