using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private Image _progressBar;

    private int _score = 0;
    private float _maxScore = 2000;


    void Update()
    {
        StartTimer();
        ClickMous();
    }
    private void StartTimer()
    {
        if (_maxScore >= _score)
        {
            _score += 1;
            _progressBar.fillAmount = _score / _maxScore;
        }
        else
        {
            // Час вийшов
        }
    }

    public void AddTime(int time)
    {
        if(_score >= 0)
        {
            _score -= time;
        }
        
    }

    private void ClickMous()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddTime(200);
        }
    }
}
