using UnityEngine;
using System.Collections;

public class SpawnerPointCoin : SpawnerPoint
{
    [SerializeField] private Coin _object;       

    protected override void Start()
    {
        Coin kit = Instantiate(_object, _position, Quaternion.identity);
        _player.CoinDisabled += DisableObject;
        _object.gameObject.SetActive(true);
    }

    private void DisableObject(Coin coin)
    {
        coin.gameObject.SetActive(false);
        WaitForSeconds wait = new WaitForSeconds(_delay);
        StartCoroutine(Enable(wait, coin));
    }

    private IEnumerator Enable(WaitForSeconds wait, Coin coin)
    {
        yield return wait;
        coin.gameObject.SetActive(true);
    }
}
