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

    private float _currentHealth = 100f;
    private Vector2 _distance = Vector3.zero;
    private float _koefOfSpeed = 0.01f;    
    private float _maxHealth = 100f;
    private Coroutine _damageCoroutine;    

    public int Damage => 10;
    public float Delay => 0.1f;
         
    private void Update()
    {
        Move();        
    }   

    public float GetHealth()
    {
        return _currentHealth;
    }

    public void Heal()
    {
        _currentHealth = _maxHealth;
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

    public IEnumerator TakeButtonDamage(int damage, WaitForSeconds wait)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        Debug.Log("HP игрока " + _currentHealth);
        yield return wait;
    }
}