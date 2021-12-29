using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : MonoBehaviour
{
    public EnemyMove _enemy;
    private float _slowDownDuraton;
    private float _saveSpeed;
    private float _percentage;
    private float _timer;
    public void UpdateTimer()
    {
        _timer = 0;
    }
    
    public void SlowEnemy(float percentage)
    {
        _enemy = GetComponent<EnemyMove>();
        _saveSpeed = _enemy.GetSpeed();
        _enemy.SetSpeed(_enemy.GetSpeed() * (1 - (percentage/100)));
        StartCoroutine(nameof(RemoveSlowDown));
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
                _enemy.SetSpeed(_saveSpeed);
                Destroy(this.GetComponent<Slower>());
            }
            else
            {
                _timer += Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();

        }
    }

}
