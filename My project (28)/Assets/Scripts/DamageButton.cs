using System.Collections;
using System.Collections.Generic;
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
        _damageCoroutine = StartCoroutine(health.TakeButtonDamage(_buttonDamage, wait));
    }
}
