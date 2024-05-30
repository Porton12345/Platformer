using System;
using UnityEngine;

public class Vampirism : MonoBehaviour
{    
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _playerLayer;

    public event Action<EnemyPatroller> HealthSucked;

    public float VampireDelay => 6f;
    public int VampireDamage => 5;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SuckHealth();
    }      

    private void SuckHealth()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, _playerLayer);
        
        foreach (Collider2D hit in hits)
        {
            if(hit.transform.TryGetComponent(out EnemyPatroller enemy))
            {
                HealthSucked?.Invoke(enemy);                
            }
        }
    }
}
