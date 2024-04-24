using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthDisplay : MonoBehaviour
{
    public Health health;

    private float _currentHealth;

    private void Update()
    {
        _currentHealth = health.GetHealth();
        ShowHealth(_currentHealth);
    }

    protected abstract void ShowHealth(float currentHealth);
}
