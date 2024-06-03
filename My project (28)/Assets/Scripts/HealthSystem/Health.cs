using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{  
    [SerializeField] private float _maxHealth = 100f;    

    public event UnityAction HealthChanged;    

    public float CurrentHealth { get;  private set; } = 100f;

    public void FullHeal()
    {
        if (_maxHealth > 0)
        {
            CurrentHealth += _maxHealth;
            HealthChanged?.Invoke();           
        }
    }

    public void TakeHeal(float heal)
    {
        if (heal > 0)
        {
            CurrentHealth += heal;
            HealthChanged?.Invoke();            
        }        
    }   

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, _maxHealth);
            HealthChanged?.Invoke();
        }             
    }
}
