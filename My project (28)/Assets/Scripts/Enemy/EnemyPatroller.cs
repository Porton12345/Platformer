using UnityEngine;
using System.Collections;

public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private Health _health;    
    [SerializeField] private Transform[] _places;
    [SerializeField] private Transform _placesPoints;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _clipRunRight;
    [SerializeField] private AnimationClip _clipRunLeft;
    [SerializeField] private LayerMask _playerLayer;

    private float _minLenght = 0.2f;
    private float _raycastDistance = 5f;
    private Coroutine _coroutine;
    private int _nextPointIndex = 0;

    public int Damage => 10;
    public float Delay => 0.1f;

    private void Start()
    {
       
        _places = new Transform[_placesPoints.childCount];

        for (int i = 0; i < _places.Length; i++)
            _places[i] = _placesPoints.GetChild(i).transform;
    }    

    private void FixedUpdate()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, _raycastDistance, _playerLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, _raycastDistance, _playerLayer);

        if (hitRight.collider == null && hitLeft.collider == null)
        {
            Patrol();
        }        

        if (_health.CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }         
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Interactor player))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            else
            {
                WaitForSeconds wait = new WaitForSeconds(player.Delay);
                _coroutine = StartCoroutine(TakeDamage(player.Damage, wait));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Interactor player))
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
        Transform target = _places[_nextPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < _minLenght)
        {
            _nextPointIndex = _nextPointIndex == 0 ? 1 : 0;
        }

        if (_nextPointIndex == 0)
        {            
            _animator.Play(_clipRunRight.name);
        }
        else if (_nextPointIndex == 1)
        {            
            _animator.Play(_clipRunLeft.name);
        }        
    }    

    private IEnumerator TakeDamage(int damage, WaitForSeconds wait)
    {
        while (true)
        {
            _health.TakeDamage(damage);            
            yield return wait;
        }        
    }
}