using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{    
    [SerializeField] private SpawnerPoints[] _spawningPoint;
    [SerializeField, Min(0)] private float _countSecondsBeforeSpawp; 

    private void Start()
    {
        var wait = new WaitForSeconds(_countSecondsBeforeSpawp);
        StartCoroutine(Spawning(wait));
    }

    private IEnumerator Spawning(WaitForSeconds wait)
    {
        while (true)
        {
            ActivatePoint();
            yield return wait;
        }
    }

    private void ActivatePoint()
    {
        int currentSpawnPoint = Random.Range(0, _spawningPoint.Length);
        SpawnerPoints SpawnerPoint = _spawningPoint[currentSpawnPoint];
        SpawnerPoint.Spawn();
    }
}
