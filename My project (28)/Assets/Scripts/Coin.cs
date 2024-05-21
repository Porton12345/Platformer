using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Disable;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Mover player))
        {
            Disable?.Invoke(this);            
        }
    }    
}
