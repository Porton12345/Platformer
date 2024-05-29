using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSliderSmooth : MonoBehaviour
{
    [SerializeField] private Slider _hpSmoothSlider;
    [SerializeField] private Health _health;
    [SerializeField] private float _smoothDecreaseDuration = 0.5f;

    private float _currentSmoothHealth;        
    private float _delay = 0.001f;        
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _health.CurrentHealth;
        _currentSmoothHealth = _health.CurrentHealth;
    }

    private void OnEnable()
    {
        _health.HealthChanged += DisplayChange;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= DisplayChange;
    }

    private void DisplayChange()
    {
        _currentHealth = _health.CurrentHealth;
        ShowHealth(_currentHealth);
    }

    private void ShowHealth(float currentHealth)
    {        
        WaitForSeconds wait = new WaitForSeconds(_delay);
        StopAllCoroutines();
        StartCoroutine(SmoothHealthShowing(currentHealth, wait));          
    }

    public IEnumerator SmoothHealthShowing(float currentHealth, WaitForSeconds wait)
    {
        float elapsedTime = 0f;
        float previousValue = _currentSmoothHealth;

        while (elapsedTime < _smoothDecreaseDuration)
        {            
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / _smoothDecreaseDuration;
            _currentSmoothHealth = Mathf.Lerp(previousValue, currentHealth, normalizedPosition);
            _hpSmoothSlider.value = _currentSmoothHealth;            
            yield return wait;
        }
    }
}
