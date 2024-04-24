using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth { get; private set; }
    public float maxHealth => 100f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void Heal()
    {
        currentHealth = maxHealth;
    }

    public IEnumerator TakeButtonDamage(int damage, WaitForSeconds wait)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log("HP игрока " + currentHealth);
        yield return wait;
    }

    public IEnumerator TakeDamage(int damage, WaitForSeconds wait)
    {
        while (true)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            Debug.Log("HP игрока " + currentHealth);
            yield return wait;
        }
    }
}
