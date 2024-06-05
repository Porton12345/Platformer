using UnityEngine;
using System.Collections;

public class EnemyPatroller : MonoBehaviour
{   
    [SerializeField] private Transform[] _places;
    [SerializeField] private Transform _placesPoints;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _clipRunRight;
    [SerializeField] private AnimationClip _clipRunLeft;
    [SerializeField] private LayerMask _playerLayer;    

    private float _minLenght = 0.2f;
    private float _raycastDistance = 5f;
    private int _nextPointIndex = 0;

    private void Start()
    {        
        _places = new Transform[_placesPoints.childCount];

        for (int i = 0; i < _places.Length; i++)
            _places[i] = _placesPoints.GetChild(i);
    }    

    private void FixedUpdate()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, _raycastDistance, _playerLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, _raycastDistance, _playerLayer);

        if (hitRight.collider == null && hitLeft.collider == null)
        {
            Patrol();
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
}