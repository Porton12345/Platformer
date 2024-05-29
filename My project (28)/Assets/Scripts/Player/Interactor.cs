using System;
using System.Collections;
using UnityEngine;

public class Interactor : MonoBehaviour
{  
    [SerializeField] private Health _health;       
   
    private Coroutine _damageCoroutine;

    public event Action<Coin> DisableCoin;
    public event Action<FirstAidKit> DisableKit;

    public int Damage => 10;
    public float Delay => 0.1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            DisableCoin?.Invoke(coin);
        }

        if (collision.gameObject.TryGetComponent(out FirstAidKit firstAidKit))
        {
            DisableKit?.Invoke(firstAidKit);
            _health.Heal();
        }

        if (collision.gameObject.TryGetComponent(out EnemyPatroller enemy))
        {
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }

            if (_damageCoroutine == null)
            {
                WaitForSeconds wait = new WaitForSeconds(enemy.Delay);
                StopAllCoroutines();
                _damageCoroutine = StartCoroutine(TakeDamage(enemy.Damage, wait));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collisiion)
    {
        if (collisiion.gameObject.TryGetComponent(out EnemyPatroller enemy))
        {
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }
        }
    }

    public IEnumerator TakeDamage(int damage, WaitForSeconds wait)
    {
        while (true)
        {
            _health.TakeDamage(damage);
            yield return wait;
        }
    }
}