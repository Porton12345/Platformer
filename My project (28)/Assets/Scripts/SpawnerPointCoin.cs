using UnityEngine;
using System.Collections;
using UnityEngine.Pool;

public class SpawnerPointCoin : MonoBehaviour
{
    [SerializeField] private Coin _coin;    
    [SerializeField] private int _coinPoolCapacity = 1;
    [SerializeField] private int _coinMaxSizePool = 1;
    [SerializeField] private Vector2 _position;

    private ObjectPool<Coin> _coinPool;
    private float _delay = 2f;

    private void Awake()
    {
        _coinPool = new ObjectPool<Coin>(CreatePooledCoin, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _coinPoolCapacity, _coinMaxSizePool);
    }
    private void Start()
    {
        _coinPool.Get();
    }
        
    private void ReturnCoinToPool(Coin coin)
    {
        _coinPool.Release(coin);
        WaitForSeconds wait = new WaitForSeconds(_delay);
        Coroutine couroutine = StartCoroutine(EnableCoin(wait, coin));
    }

    private Coin CreatePooledCoin()
    {
        Coin coin = Instantiate(_coin, _position, Quaternion.identity);
        coin.Disable += ReturnCoinToPool;

        coin.gameObject.SetActive(false);

        return coin;
    }

    private void OnTakeFromPool(Coin coin)
    {
        coin.gameObject.SetActive(true);
    }

    private void OnReturnToPool(Coin coin)
    {
        coin.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Coin coin)
    {
        Destroy(coin.gameObject);
    }     

    private IEnumerator EnableCoin(WaitForSeconds wait, Coin coin)
    {
        yield return wait;
        Debug.Log("Монета вкл");
        coin.gameObject.SetActive(true);
    }
}
