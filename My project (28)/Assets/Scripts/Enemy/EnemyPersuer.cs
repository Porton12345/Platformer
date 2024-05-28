using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPersuer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private AnimationClip _clipRunRight;
    [SerializeField] private AnimationClip _clipRunLeft;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Mover _playerPosition;
        
    private float _raycastDistance = 5f;

    private void FixedUpdate()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, _raycastDistance, _playerLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, _raycastDistance, _playerLayer);
                
        if (hitRight.collider != null)
        {
            Pursuit(_clipRunLeft);
        }
        else if (hitLeft.collider != null)
        {
            Pursuit(_clipRunRight);
        }        
    }

    private void Pursuit(AnimationClip clip)    
    {        
        transform.position = Vector2.MoveTowards(transform.position, _playerPosition.transform.position, _speed * Time.deltaTime);
        _animator.Play(clip.name);
    }
}
