using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderSmooth : HealthDisplay
{
    [SerializeField] private Slider _hpSmoothSlider;

    private float _currentSmoothHealth;
    private float _maxDelta = 100f;

    protected override void ShowHealth(float currentHealth)
    {
        _currentSmoothHealth = Mathf.MoveTowards(_currentSmoothHealth, currentHealth, _maxDelta * Time.deltaTime);
        _hpSmoothSlider.value = _currentSmoothHealth;
    }
}
