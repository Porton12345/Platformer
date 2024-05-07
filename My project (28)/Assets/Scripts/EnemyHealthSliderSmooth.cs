using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealthSliderSmooth : MonoBehaviour
{
    [SerializeField] private Slider _hpEnemySmoothSlider;
    [SerializeField] private EnemyHealth _enemyHealth;

    private float _currentSmoothHealth;
    private float _maxDelta = 100f;
    private Coroutine _smoothCoroutine;
    private float _delay = 0.001f;   
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _enemyHealth.ÑurrentEnemyHealth;
        _currentSmoothHealth = _enemyHealth.ÑurrentEnemyHealth;
    }

    private void OnEnable()
    {
        _enemyHealth.OnEnemyHealthChange += DisplayChange;
    }

    private void OnDisable()
    {
        _enemyHealth.OnEnemyHealthChange -= DisplayChange;
    }

    private void DisplayChange()
    {
        _currentHealth = _enemyHealth.GetEnemyHealth();
        ShowHealth(_currentHealth);
    }

    public void ShowHealth(float currentHealth)
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
            Debug.Log("Enemy smooth HP " + _currentSmoothHealth);
            _hpEnemySmoothSlider.value = _currentSmoothHealth;
            yield return wait;
        }
    }
}
