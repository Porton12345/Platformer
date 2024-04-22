using UnityEngine;
using System.Collections;

public class EnemyMover : MonoBehaviour
{   
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _clipRunRight;
    [SerializeField] private AnimationClip _clipRunLeft;
    [SerializeField] private LayerMask _layerMask;

    private int _firstWaypoint = 0;
    private int _secondWaypoint = 1;
    private int _currentWaypoint;
    private float _minLenght = 0.2f;
    private float _raycastDistance = 5f;
    private Coroutine _coroutine;
    private int _currentHealth = 100;
    private int _maxHealth = 100;

    public int Damage => 10;
    public float Delay => 0.1f;

    private void Start()
    {
        _currentWaypoint = _firstWaypoint;
    }    

    private void FixedUpdate()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, _raycastDistance, _layerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, _raycastDistance, _layerMask);

        if (hitRight.collider == null & hitLeft.collider == null)
        {
            Patrol();
        }
        else if (hitRight.collider != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _waypoints[_secondWaypoint].position, _speed * Time.deltaTime);
            _animator.Play(_clipRunLeft.name);

        }
        else if (hitLeft.collider != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _waypoints[_firstWaypoint].position, _speed * Time.deltaTime);
            _animator.Play(_clipRunRight.name);
        }

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            else if (_coroutine == null)
            {
                WaitForSeconds wait = new WaitForSeconds(player.Delay);
                _coroutine = StartCoroutine(TakeDamage(player.Damage, wait));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _waypoints[_currentWaypoint].position) < _minLenght)
        {
            if (_currentWaypoint == _firstWaypoint)
            {
                _currentWaypoint = _secondWaypoint;
            }
            else if (_currentWaypoint == _secondWaypoint)
            {
                _currentWaypoint = _firstWaypoint;
            }
        }

        if (_currentWaypoint == _firstWaypoint)
            _animator.Play(_clipRunRight.name);
        if (_currentWaypoint == _secondWaypoint)
            _animator.Play(_clipRunLeft.name);
    }

    private IEnumerator TakeDamage(int damage, WaitForSeconds wait)
    {
        while (true)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            Debug.Log("HP слайма " + _currentHealth);
            yield return wait;
        }
    }
}