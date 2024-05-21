using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public event UnityAction OnHealthChange; 
    
    public float CurrentHealth { get;  private set; } = 100f;
    public float MaxHealth => 100f;      

    public void Heal()
    {
        CurrentHealth = MaxHealth;
        OnHealthChange?.Invoke();
    }    

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        OnHealthChange?.Invoke();
    }
}
