using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Health _health;
   
    private Coroutine _coroutine;  

    public int Damage => 10;
    public float Delay => 0.1f;

    private void OnEnable()
    {
        _health.HealthChanged += CheckDeath;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= CheckDeath;
    }       

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            else
            {
                WaitForSeconds wait = new WaitForSeconds(player.Delay);
                _coroutine = StartCoroutine(TakeDamage(player.Damage, wait));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }

    public float GiveHealthRemainder(float damage)
    {
        float result = _health.CurrentHealth - damage;

        if (result > 0)
        {
            return damage;
        }
        else
        {
            return _health.CurrentHealth;
        }
    }

    private void CheckDeath()
    {
        if (_health.CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }        

    private IEnumerator TakeDamage(int damage, WaitForSeconds wait)
    {
        while (true)
        {
            _health.TakeDamage(damage);
            yield return wait;
        }
    }
}
