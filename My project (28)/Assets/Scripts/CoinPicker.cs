using Unity.VisualScripting;
using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.GetComponent<Coin>())
        {
            Destroy(collision.gameObject);
        }           
    }
}
