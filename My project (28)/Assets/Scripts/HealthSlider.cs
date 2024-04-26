using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : HealthDisplay
{
    [SerializeField] private Slider _hpSlider;

    protected override void ShowHealth(float currentHealth)
    {
        _hpSlider.value = currentHealth;
    }
}
