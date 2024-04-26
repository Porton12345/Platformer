using UnityEngine;

public class HealingButton : MonoBehaviour
{
    public Health health;

    public void OnClickHealButton()
    {
        health.Heal();
    }
}
