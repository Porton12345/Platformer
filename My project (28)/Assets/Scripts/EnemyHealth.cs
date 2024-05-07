using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public event UnityAction OnEnemyHealthChange;

    public float ÑurrentEnemyHealth { get; private set; } = 100f;
    public float MaxEnemyHealth => 100f;

    public float GetEnemyHealth()
    {
        return ÑurrentEnemyHealth;
    }
    
    public void GetDamage(int damage)
    {
        ÑurrentEnemyHealth = Mathf.Clamp(ÑurrentEnemyHealth - damage, 0, MaxEnemyHealth);
        OnEnemyHealthChange?.Invoke();
    }
}
