using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{  
    [SerializeField] private Health _health;       
   
    private Coroutine _damageCoroutine;        

    public event Action<Coin> CoinDisabled;
    public event Action<FirstAidKit> KitDisabled;

    public int Damage => 10;
    public float Delay => 0.1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            CoinDisabled?.Invoke(coin);
        }

        if (collision.gameObject.TryGetComponent(out FirstAidKit firstAidKit))
        {
            KitDisabled?.Invoke(firstAidKit);
            _health.FullHeal();
        }

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }

            if (_damageCoroutine == null)
            {
                WaitForSeconds wait = new WaitForSeconds(enemy.Delay);                
                _damageCoroutine = StartCoroutine(TakeDamage(enemy.Damage, wait));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collisiion)
    {
        if (collisiion.gameObject.TryGetComponent(out Enemy enemy))
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
