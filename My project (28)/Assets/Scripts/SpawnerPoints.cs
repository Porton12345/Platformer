using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerPoints : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Vector2 _position;
    private float _spawnRadius = 1.0f;

    public void Spawn()
    {
        if (CanSpawnCoin(_position))
        {
            Coin coin = Instantiate(_coin, _position, Quaternion.identity);
        }                                    
    }

    private bool CanSpawnCoin(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _spawnRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag(_coin.tag))
            {
                return false;
            }
        }

        return true;
    }
}
