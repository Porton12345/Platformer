using UnityEngine;

public abstract class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health health;

    protected float _currentHealth;

    private void Start()
    {
        _currentHealth = health.CurrentHealth;
    }

    private void OnEnable()
    {
        health.OnHealthChange += DisplayChange;
    }

    private void OnDisable()
    {
        health.OnHealthChange -= DisplayChange;
    }        

    private void DisplayChange()
    {
        _currentHealth = health.CurrentHealth;
        ShowHealth(_currentHealth);
    }

    protected abstract void ShowHealth(float currentHealth);
}
