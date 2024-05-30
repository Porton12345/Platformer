using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{  
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Vampirism _player;

    public event UnityAction HealthChanged;

    public float CurrentHealth { get;  private set; } = 100f;

    private void Start()
    {
        if(this.TryGetComponent<Vampirism>(out Vampirism player))
            _player.HealthSucked += VampirismHeal;
    }        

    public void FullHeal()
    {
        if (_maxHealth > 0)
        {
            CurrentHealth = _maxHealth;
            HealthChanged?.Invoke();
        }        
    }

    public void VampirismHeal(EnemyPatroller enemy)
    {
        CurrentHealth += _player.VampireDamage;
        HealthChanged?.Invoke();        
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
