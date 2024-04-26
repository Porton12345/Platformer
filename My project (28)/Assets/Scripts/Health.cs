using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public event UnityAction OnHealthChange; 
    
    public float �urrentHealth { get;  set; } = 100f;
    public float MaxHealth => 100f;
        
    public float GetHealth()
    {       
        return �urrentHealth;
    }

    public void Heal()
    {
        �urrentHealth = MaxHealth;
        OnHealthChange?.Invoke();
    }    

    public void GetDamage(int damage)
    {
        �urrentHealth = Mathf.Clamp(�urrentHealth - damage, 0, MaxHealth);
        OnHealthChange?.Invoke();
    }
}
