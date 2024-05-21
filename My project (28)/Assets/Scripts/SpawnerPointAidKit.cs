using UnityEngine;
using System.Collections;
using UnityEngine.Pool;

public class SpawnerPointAidKit : MonoBehaviour
{
    [SerializeField] private FirstAidKit _kit;
    [SerializeField] private int _kitPoolCapacity = 1;
    [SerializeField] private int _kitMaxSizePool = 1;
    [SerializeField] private Vector2 _position;

    private ObjectPool<FirstAidKit> _kitPool;
    private float _delay = 2f;

    private void Awake()
    {
        _kitPool = new ObjectPool<FirstAidKit>(CreatePooledKit, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _kitPoolCapacity, _kitMaxSizePool);
    }

    private void Start()
    {
        _kitPool.Get();
    }

    private void ReturnKitToPool(FirstAidKit kit)
    {
        _kitPool.Release(kit);
        WaitForSeconds wait = new WaitForSeconds(_delay);
        Coroutine couroutine = StartCoroutine(EnableKit(wait, kit));
    }

    private FirstAidKit CreatePooledKit()
    {
        FirstAidKit kit = Instantiate(_kit, _position, Quaternion.identity);
        kit.Disable += ReturnKitToPool;

        kit.gameObject.SetActive(false);

        return kit;
    }

    private void OnTakeFromPool(FirstAidKit kit)
    {
        kit.gameObject.SetActive(true);
    }

    private void OnReturnToPool(FirstAidKit kit)
    {
        kit.gameObject.SetActive(false);
    }

    private void OnDestroyObject(FirstAidKit kit)
    {
        Destroy(kit.gameObject);
    }

    private IEnumerator EnableKit(WaitForSeconds wait, FirstAidKit kit)
    {
        yield return wait;
        Debug.Log("������� ���");
        kit.gameObject.SetActive(true);
    }
}
