using UnityEngine;

public abstract class SpawnerPoint : MonoBehaviour
{
    [SerializeField] protected Vector2 _position;    
    [SerializeField] protected Player _player;

    protected float _delay = 2f;

    protected abstract void Start();      
}
