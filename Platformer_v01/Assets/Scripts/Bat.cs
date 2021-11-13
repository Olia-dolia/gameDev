using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private float _walkRange;
    [SerializeField] private bool _faceRight;
    [SerializeField] private int _damage;
    [SerializeField] private int _pushPower;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _flyAnimatorKey;

    private Vector2 _startPosition;
    private int _direction = 1;
    private float _lastAttackTime;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_faceRight && transform.position.x > _startPosition.x + _walkRange)
        {
            Flip();
        }
        else if (!_faceRight && transform.position.x < _startPosition.x - _walkRange)
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {
        Collider2D player = Physics2D.OverlapBox(transform.position, new Vector2(_walkRange, 1), 0, _whatIsPlayer);
        if(player != null)
        {
            
            _rigidbody2D.velocity = Vector2.right * _direction * _speed;
            _animator.SetBool(_flyAnimatorKey, true);
        }
        
    }
    
    private Vector2 _drawPosition
    {
        get
        {
            if (_startPosition == Vector2.zero)
            {
                return transform.position;
            }
            else return _startPosition;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_drawPosition, new Vector3(_walkRange * 2, 1, 0));
    }
    private void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
        _direction *= -1;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Movement_controller player = other.collider.GetComponent<Movement_controller>();
        if (player != null && Time.time + _lastAttackTime > 0.02f)
        {
            _lastAttackTime = Time.time;
            player.TakeDamage(_damage, _pushPower, transform.position.x);
        }
    }
}
