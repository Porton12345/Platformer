using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingButton : MonoBehaviour
{
    public Health health;

    public void OnClickHealButton()
    {
        health.Heal();
    }
}
