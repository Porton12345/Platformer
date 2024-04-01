using UnityEngine;

public class SpawnerPointsAidKit : MonoBehaviour
{
    [SerializeField] private FirstAidKit _firstAidKit;
    [SerializeField] private Vector2 _position;

    private float _spawnRadius = 1.0f;

    public void Spawn()
    {
        if (CanSpawnKit(_position))
        {
            FirstAidKit firstAidkit = Instantiate(_firstAidKit, _position, Quaternion.identity);
        }
    }

    private bool CanSpawnKit(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _spawnRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag(_firstAidKit.tag))
            {
                return false;
            }
        }

        return true;
    }
}
