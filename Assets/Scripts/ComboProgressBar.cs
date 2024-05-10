using UnityEngine;
using UnityEngine.UI;

public class ComboProgressBar : MonoBehaviour
{
    [SerializeField] private Image _progressBar;

    private float _maxComboPoint= 2500;
    private float _currentComboPoints = 0;


    void Update()
    {
        ComboProgress();
    }

    private void ComboProgress()
    {
        if (_currentComboPoints > 0) 
        {
            _currentComboPoints -= 2;
            _progressBar.fillAmount = _currentComboPoints / _maxComboPoint; 
        }

        if (_currentComboPoints <= 0) 
        {
            _progressBar.fillAmount = 0; 
        }

        if (_currentComboPoints >= _maxComboPoint)
        {
            Debug.Log("Combo is over");
        }
    }

    public void AddComboPoints(int points)
    {
        if(_currentComboPoints >= _maxComboPoint)
        {
            return;
        }
        _currentComboPoints += points;
    }
}
