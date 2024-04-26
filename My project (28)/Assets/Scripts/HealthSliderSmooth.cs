using UnityEngine;
using UnityEngine.UI;

public class HealthSliderSmooth : HealthDisplay
{
    [SerializeField] private Slider _hpSmoothSlider;

    private float _currentSmoothHealth;
    private float _maxDelta = 100f;        

    private void Update()
    {        
        ShowHealth(_currentHealth);
    }

    protected override void ShowHealth(float currentHealth)
    {
        _currentSmoothHealth = Mathf.MoveTowards(_currentSmoothHealth, currentHealth, _maxDelta * Time.deltaTime);
        Debug.Log("Smooth HP " + _currentSmoothHealth);
        _hpSmoothSlider.value = _currentSmoothHealth;
    }
}
