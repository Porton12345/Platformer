using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPersuer : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationClip _clipRunRight;
    [SerializeField] private AnimationClip _clipRunLeft;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Animator _animator;

    private int _firstWaypoint = 0;
    private int _secondWaypoint = 1;
    private float _raycastDistance = 5f;

    private void FixedUpdate()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, _raycastDistance, _playerLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, _raycastDistance, _playerLayer);
                
        if (hitRight.collider != null)
        {
            Pursuit(_secondWaypoint, _clipRunLeft);
        }
        else if (hitLeft.collider != null)
        {
            Pursuit(_firstWaypoint, _clipRunRight);
        }        
    }

    private void Pursuit(int waypoint, AnimationClip clip)
    {
        transform.position = Vector2.MoveTowards(transform.position, _waypoints[waypoint].position, _speed * Time.deltaTime);
        _animator.Play(clip.name);
    }
}
