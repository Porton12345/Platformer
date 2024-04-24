using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : HealthDisplay
{
    [SerializeField] private TextMeshProUGUI _hpText;    
        
    protected override void ShowHealth(float currentHealth)
    {
        _hpText.text = currentHealth.ToString("") + "/100";
    }
}
