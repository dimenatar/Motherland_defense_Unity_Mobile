using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpeedGameUp : MonoBehaviour
{
    [SerializeField] private Text _speedText;
    public enum Speed
    {
        Default,
        Double,
        Quadruple
    }

    private static Speed _currentSpeed = Speed.Default;
    public static Dictionary<Speed, int> Speeds = new Dictionary<Speed, int>() { { Speed.Default, 1 }, { Speed.Double, 2 }, { Speed.Quadruple, 4 } };

    public static Speed CurrentSpeed => _currentSpeed;

    public void ChangeSpeed()
    {
        if (_currentSpeed != Speed.Quadruple)
        {
            _currentSpeed++;
        }
        else
        {
            _currentSpeed = 0;
        }
        SpeedUp(Speeds.Where(key => key.Key == _currentSpeed).Select(value => value.Value).FirstOrDefault());
    }

    private void SpeedUp(float time)
    {
        Time.timeScale = time;
        _speedText.text = time.ToString() + "X";
    }
}
