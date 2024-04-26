using System.Collections;
using UnityEngine;

public class DamageButton : MonoBehaviour
{
    public Health health;
        
    private Coroutine _damageCoroutine;
    private int _delay = 1;
    private int _buttonDamage = 10;    

    public void OnClickDamageButton()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        _damageCoroutine = StartCoroutine(TakeButtonDamage(_buttonDamage, wait));
    }

    public IEnumerator TakeButtonDamage(int damage, WaitForSeconds wait)
    {
        health.GetDamage(damage); 
        Debug.Log("HP èãðîêà " + health.ÑurrentHealth);
        yield return wait;
    }
}
