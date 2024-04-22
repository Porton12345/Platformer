using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);    

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _clipIdle;
    [SerializeField] private AnimationClip _clipRunRight;
    [SerializeField] private AnimationClip _clipRunLeft;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _hpSmoothSlider;

    private Vector2 _distance = Vector3.zero;
    private float _koefOfSpeed = 0.01f;
    private float _currentHealth = 100f;
    private float _maxHealth = 100f;
    private Coroutine _damageCoroutine;
    private float _maxDelta = 100f;
    private int _delay = 1;
    private int _buttonDamage = 10;
    private float _currentSmoothHealth;

    public int Damage => 10;
    public float Delay => 0.1f;

    public void OnClickDamageButton()
    {               
        WaitForSeconds wait = new WaitForSeconds(_delay);
        _damageCoroutine = StartCoroutine(TakeButtonDamage(_buttonDamage, wait));        
    }

    public void OnClickHealButton()
    {
        _currentHealth = _maxHealth;       
    }  

    private void Start()
    {
        _hpText.text = "100/100";
        _currentSmoothHealth = _maxHealth;
    }

    private void Update()
    {
        Move();
        _hpText.text = _currentHealth.ToString("") + "/100";
        _hpSlider.value = _currentHealth;           
        _currentSmoothHealth = Mathf.MoveTowards(_currentSmoothHealth, _currentHealth, _maxDelta * Time.deltaTime);
        _hpSmoothSlider.value = _currentSmoothHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.TryGetComponent(out FirstAidKit firstAidKit))
        {
            Destroy(collision.gameObject);
            _currentHealth = _maxHealth;            
        }       

        if (collision.gameObject.TryGetComponent(out EnemyMover enemy))
        {
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }
            
            if (_damageCoroutine == null)
            {                
                WaitForSeconds wait = new WaitForSeconds(enemy.Delay);
                _damageCoroutine = StartCoroutine(TakeDamage(enemy.Damage, wait));
            }
        }
    }      

    private void OnTriggerExit2D(Collider2D collisiion)
    {
        if (collisiion.gameObject.TryGetComponent(out EnemyMover enemy))
        {
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }
        }
    }

    private void Move()
    {
        float verticalDirection = Input.GetAxisRaw(Vertical);
        Vector2 verticalDistance = verticalDirection * _moveSpeed * Time.deltaTime * Vector3.up;

        float horizontalDirection = Input.GetAxisRaw(Horizontal);
        Vector2 horizontalDistance = horizontalDirection * _moveSpeed * Time.deltaTime * Vector3.right;

        _distance = (verticalDistance + horizontalDistance).normalized * _koefOfSpeed;
        transform.Translate(_distance);

        if (_distance == Vector2.zero)
        {
            _animator.Play(_clipIdle.name);
        }
        else if (horizontalDirection > 0)
        {
            _animator.Play(_clipRunRight.name);
        }
        else if (horizontalDirection < 0)
        {
            _animator.Play(_clipRunLeft.name);
        }
    }

    private IEnumerator TakeDamage(int damage, WaitForSeconds wait)
    {       
        while (true)
        {            
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            Debug.Log("HP игрока " + _currentHealth);            
            yield return wait;
        }
    }

    private IEnumerator TakeButtonDamage(int damage, WaitForSeconds wait)
    {        
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            Debug.Log("HP игрока " + _currentHealth);
            yield return wait;        
    }
}