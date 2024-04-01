using System.Collections;
using UnityEngine;

public class FirstAidKitSpawner : MonoBehaviour
{    
    [SerializeField] private SpawnerPointsAidKit[] _spawningPoint;
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
        int _currentSpawnPoint = Random.Range(0, _spawningPoint.Length);
        SpawnerPointsAidKit SpawnerPoint = _spawningPoint[_currentSpawnPoint];
        SpawnerPoint.Spawn();
    }
}
