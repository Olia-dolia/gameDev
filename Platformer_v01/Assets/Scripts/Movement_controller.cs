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
    [SerializeField] private float _shieldTime;
    //[SerializeField] private float _currentTime;

    [Header(("Animator"))]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _runAnimatorKey;
    [SerializeField] private string _jumpAnimatorKey;
    [SerializeField] private string _crouchAnimatorKey;


    private float _move;
    private bool _jump;
    private bool _isGrounded;
    bool _canStand;

    public bool Fireball { private get; set; }
    public bool Shield { private get; set; }

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

        _animator.SetFloat(_runAnimatorKey, Mathf.Abs(_move));

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
       _playerRB.velocity = new Vector2(_speed * _move, _playerRB.velocity.y);

        if (Fireball)
        {
            //We can use fireball for Attack
        }
        if (Shield)
        {   
            _shieldTime -= Time.deltaTime;
                if (_shieldTime <= 0)
                {
                    Debug.Log("Your time is up!");
                    Shield = false;
                }
        }

        if(_move != 0 && (_isGrounded || _airMove))
        {
            _playerRB.velocity = new Vector2(_speed * _move, _playerRB.velocity.y); ;
        }
        
        if (_jump && _isGrounded)
        {
            _playerRB.AddForce(Vector2.up * _jumpForce);
            _jump = false;
        }
                
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _radius, _whatIsGround);
        _canStand = !Physics2D.OverlapCircle(_headChecker.position, _radius, _whatIsGround);

        _animator.SetBool(_jumpAnimatorKey, !_isGrounded);
        _animator.SetBool(_crouchAnimatorKey, !_headCollider.enabled);
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

    public void AddHP(int hpPoints)
    {
        Debug.Log("hp increase" + hpPoints);
    }
}
