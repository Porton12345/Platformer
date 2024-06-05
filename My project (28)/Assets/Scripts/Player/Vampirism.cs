using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Vampirism : MonoBehaviour
{        
    [SerializeField] private LayerMask _playerLayer;    
    [SerializeField] private Slider _vampirismCooldowmSlider;
    [SerializeField] private Health _health;

    private Coroutine _coroutine;
    private int _number = 6;
    private float _delay = 1f;      
    
    public float Radius => 5f;
    public float VampireDelay => 6f;
    public int VampireDamage => 7;

    private void Update()
    {       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseVampirism();
        }            
    }        

    private void UseVampirism()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);        

       if (_coroutine == null)
        {            
            _coroutine = StartCoroutine(Countdown(wait));            
        }        
    }

    private void SuckHealth()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, Radius, _playerLayer);
        
        foreach (Collider2D hit in hits)
        {
            if(hit.transform.TryGetComponent(out Enemy enemy))
            {
                _health.TakeHeal(enemy.GiveHealthRemainder(VampireDamage));

                if (enemy.TryGetComponent(out Health health))
                {
                    health.TakeDamage(VampireDamage);
                }                         
            }
        }
    }

    private IEnumerator Countdown(WaitForSeconds wait)
    {        
        while (true)
        {
            yield return wait;
            _number--;                       

            if (_number == 0)
            {
                _number = 6;
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            SuckHealth();
            _vampirismCooldowmSlider.value = _number;
        }
    }    
}
