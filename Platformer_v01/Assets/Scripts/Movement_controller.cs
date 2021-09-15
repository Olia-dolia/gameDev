using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement_controller : MonoBehaviour
{
    Rigidbody2D _playerRB;
    [SerializeField] private float _speed;
    [SerializeField] private bool _spriteFlip;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _radius;
    [SerializeField] private bool _airMove;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _whatIsGround;
    [SerializeField] Collider2D _headCollider;
    [SerializeField] Transform _headChecker;
    private float _move;
    private bool _jump;
    private bool _isGrounded;
    bool _canStand;

    private void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");

        if (_move > 0 && _spriteFlip)
        {
            Flip();
        }
        else if (_move < 0 && !_spriteFlip)
        {
            Flip();
        }

        if (Input.GetButtonUp("Jump"))
        {
            _jump = true;
        };
        if (Input.GetKey(KeyCode.C)) {
            _headCollider.enabled = false;
        }
        else if (!Input.GetKey(KeyCode.C) && _canStand)
        {
            _headCollider.enabled = true;
        }
        
    }

    private void FixedUpdate()
    {
        if(_move != 0 && (_isGrounded || _airMove))
        {
            _playerRB.velocity = new Vector2(_speed * _move, _playerRB.velocity.y); ;
        }
        _playerRB.velocity = new Vector2(_speed * _move, _playerRB.velocity.y);
        if (_jump && _isGrounded)
        {
            _playerRB.AddForce(Vector2.up * _jumpForce);
            _jump = false;
        }
                
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _radius, _whatIsGround);
        _canStand = !Physics2D.OverlapCircle(_headChecker.position, _radius, _whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_headChecker.position, _radius);
    }
    void Flip()
    {
        _spriteFlip = !_spriteFlip;
        transform.Rotate(0f, 180f, 0f);
    }
}
