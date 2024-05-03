using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSliderSmooth : HealthDisplay
{
    [SerializeField] private Slider _hpSmoothSlider;

    private float _currentSmoothHealth;
    private float _maxDelta = 100f;
    private Coroutine _smoothCoroutine;
    private float _delay = 0.001f;    

    protected override void ShowHealth(float currentHealth)
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
