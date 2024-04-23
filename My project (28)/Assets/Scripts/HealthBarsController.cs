using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarsController : MonoBehaviour
{
    public PlayerController playerController;

    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _hpSmoothSlider;

    private float _currentHealth;    
    private float _maxDelta = 100f;
    private Coroutine _damageCoroutine;
    private int _delay = 1;
    private int _buttonDamage = 10;
    private float _currentSmoothHealth;

    private void Start()
    {
        _hpText.text = "100/100";       
    }

    private void Update()
    {
        _currentHealth = playerController.GetHealth();
        _hpText.text = _currentHealth.ToString("") + "/100";
        _hpSlider.value = _currentHealth;
        _currentSmoothHealth = Mathf.MoveTowards(_currentSmoothHealth, _currentHealth, _maxDelta * Time.deltaTime);
        _hpSmoothSlider.value = _currentSmoothHealth;
    }

    public void OnClickDamageButton()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        _damageCoroutine = StartCoroutine(playerController.TakeButtonDamage(_buttonDamage, wait));
    }

    public void OnClickHealButton()
    {
        playerController.Heal();
    }   
}
