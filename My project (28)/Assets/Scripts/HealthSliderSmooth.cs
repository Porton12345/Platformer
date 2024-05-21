using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSliderSmooth : MonoBehaviour
{
    [SerializeField] private Slider _hpSmoothSlider;
    [SerializeField] private Health _health;

    private float _currentSmoothHealth;
    private float _maxDelta = 100f;
    private Coroutine _smoothCoroutine;
    private float _delay = 0.001f;        
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _health.CurrentHealth;
        _currentSmoothHealth = _health.CurrentHealth;
    }

    private void OnEnable()
    {
        _health.OnHealthChange += DisplayChange;
    }

    private void OnDisable()
    {
        _health.OnHealthChange -= DisplayChange;
    }

    private void DisplayChange()
    {
        _currentHealth = _health.CurrentHealth;
        ShowHealth(_currentHealth);
    }

    private void ShowHealth(float currentHealth)
    {         
        if (_smoothCoroutine != null)
        {
            StopCoroutine(_smoothCoroutine);
            _smoothCoroutine = null;            
        }

        if (_smoothCoroutine == null)
        {            
            WaitForSeconds wait = new WaitForSeconds(_delay);
            _smoothCoroutine = StartCoroutine(SmoothHealthShowing(currentHealth, wait));
        }        
    }

    public IEnumerator SmoothHealthShowing(float currentHealth, WaitForSeconds wait)
    {              
        while (true)
        {
            _currentSmoothHealth = Mathf.MoveTowards(_currentSmoothHealth, currentHealth, _maxDelta * Time.deltaTime);
            Debug.Log("Smooth HP " + _currentSmoothHealth);
            _hpSmoothSlider.value = _currentSmoothHealth;            
            yield return wait;
        }
    }
}
