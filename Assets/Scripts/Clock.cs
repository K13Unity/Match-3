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
    }
    private void StartTimer()
    {
        if (_maxScore > _score)
        {
            _score += 1;
            _progressBar.fillAmount = _score / _maxScore;
        }
    }
}
