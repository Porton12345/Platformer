using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;    
    [SerializeField] private AnimationClip _clipRunRight;
    [SerializeField] private AnimationClip _clipRunLeft;

    private int _firstWaypoint = 0;
    private int _secondWaypoint = 1;
    private int _currentWaypoint;
    private float _minLenght = 0.2f;

    private void Start()
    {
        _currentWaypoint = _firstWaypoint;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
        
        if(Vector2.Distance(transform.position, _waypoints[_currentWaypoint].position) < _minLenght)
        {
            if(_currentWaypoint == _firstWaypoint)
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
}
