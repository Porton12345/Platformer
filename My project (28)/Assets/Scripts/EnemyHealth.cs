using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public event UnityAction OnEnemyHealthChange;

    public float �urrentEnemyHealth { get; private set; } = 100f;
    public float MaxEnemyHealth => 100f;

    public float GetEnemyHealth()
    {
        return �urrentEnemyHealth;
    }
    
    public void GetDamage(int damage)
    {
        �urrentEnemyHealth = Mathf.Clamp(�urrentEnemyHealth - damage, 0, MaxEnemyHealth);
        OnEnemyHealthChange?.Invoke();
    }
}
