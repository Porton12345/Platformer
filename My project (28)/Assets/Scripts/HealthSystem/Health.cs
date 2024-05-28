using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{  
    [SerializeField] private float MaxHealth = 100f;

    public event UnityAction OnHealthChange;

    public float CurrentHealth { get;  private set; } = 100f;        

    public void Heal()
    {
        if (MaxHealth > 0)
        {
            CurrentHealth = MaxHealth;
            OnHealthChange?.Invoke();
        }        
    }    

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
            OnHealthChange?.Invoke();
        }             
    }
}
