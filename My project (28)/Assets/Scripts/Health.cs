using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public event UnityAction OnHealthChange; 
    
    public float ŅurrentHealth { get;  private set; } = 100f;
    public float MaxHealth => 100f;
        
    public float GetHealth()
    {       
        return ŅurrentHealth;
    }

    public void Heal()
    {
        ŅurrentHealth = MaxHealth;
        OnHealthChange?.Invoke();
    }    

    public void GetDamage(int damage)
    {
        ŅurrentHealth = Mathf.Clamp(ŅurrentHealth - damage, 0, MaxHealth);
        OnHealthChange?.Invoke();
    }
}
