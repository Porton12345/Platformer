using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
        }            
    }
}
