using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public event UnityAction OnHealthChange; 
    
    public float ÑurrentHealth { get;  set; } = 100f;
    public float MaxHealth => 100f;
        
    public float GetHealth()
    {       
        return ÑurrentHealth;
    }

    public void Heal()
    {
        ÑurrentHealth = MaxHealth;
        OnHealthChange?.Invoke();
    }    

    public void GetDamage(int damage)
    {
        ÑurrentHealth = Mathf.Clamp(ÑurrentHealth - damage, 0, MaxHealth);
        OnHealthChange?.Invoke();
    }
}
