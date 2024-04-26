using UnityEngine;

public abstract class HealthDisplay : MonoBehaviour
{
    public Health health;

    protected float _currentHealth;

    private void Start()
    {
        _currentHealth = health.ŅurrentHealth;
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
        _currentHealth = health.GetHealth();
        ShowHealth(_currentHealth);
    }

    protected abstract void ShowHealth(float currentHealth);
}
