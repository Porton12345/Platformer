using System;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    public event Action<FirstAidKit> Disable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Mover player))
        {
            Disable?.Invoke(this);
        }
    }
}