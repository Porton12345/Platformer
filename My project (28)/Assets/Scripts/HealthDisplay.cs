using UnityEngine;

public abstract class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health health;

    protected float CurrentHealth;

    private void Start()
    {
        CurrentHealth = health.CurrentHealth;
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
        CurrentHealth = health.CurrentHealth;
        ShowHealth(CurrentHealth);
    }

    protected abstract void ShowHealth(float currentHealth);
}
