using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] LayerMask _whatIsCell;
    [SerializeField] Collider2D _headCollider;
    [SerializeField] Transform _headChecker;
    [SerializeField] private int _maxHp;
    [SerializeField] private int _maxManna;


    [Header(("Animator"))]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _runAnimatorKey;
    [SerializeField] private string _jumpAnimatorKey;
    [SerializeField] private string _crouchAnimatorKey;
    [SerializeField] private string _hurtAnimatorKey;
    [SerializeField] private string _attackAnimatorKey;
    [SerializeField] private string _castAnimatorKey;

    [Header("UI")]
    [SerializeField] private TMP_Text _coinsAmountText;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _mannaBar;

    [Header("Attack")]
    [SerializeField] private int _swordDamage;
    [SerializeField] private Transform _swordAttackPoint;
    [SerializeField] private float _swordAttackRadius;
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private int _skillDamage;
    [SerializeField] private Transform _skillCastPoint;
    [SerializeField] private float _skillLength;
    [SerializeField] private LineRenderer _castLine;

    private float _Hmove;
    private float _Vmove;
    private bool _jump;
    private bool _isGrounded;
    bool _canStand;
    private int _coinsAmount;
    private int _currentHp;
    private int _currentManna;
    private float _lastPushTime;
    private bool _needToAttack;
    private bool _needToCast;

    public bool CanClimb { private get; set; }

    public bool Fireball { get; set; }
    public bool ActivateAltar { get; set; }
    public int CoinsAmount
    {
        get => _coinsAmount;
        set
        {
            _coinsAmount = value;
            _coinsAmountText.text = value.ToString();
        }
    }
    private int CurrentHp
    {
        get => _currentHp;
        set
        {
            _currentHp = value;
            _hpBar.value = value;
        }
    }
    private int CurrentManna
    {
        get => _currentManna;
        set
        {
            _currentManna = value;
            _mannaBar.value = value;
        }
    }
    private void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        CoinsAmount = 0;
        _hpBar.maxValue = _maxHp;
        CurrentHp = _maxHp;
        _mannaBar.maxValue = _maxManna;
        CurrentManna = 0;
    }

    
    void Update()
    {
        _Hmove = Input.GetAxisRaw("Horizontal");
        _Vmove = Input.GetAxisRaw("Vertical");
        _animator.SetFloat(_runAnimatorKey, Mathf.Abs(_Hmove));
        if (_animator.GetBool(_hurtAnimatorKey))
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            _needToAttack = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            _needToCast = true;
        }

        if (_Hmove > 0 && _spriteFlip)
        {
            Flip();
        }
        else if (_Hmove < 0 && !_spriteFlip)
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
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _radius, _whatIsGround);
        if (_animator.GetBool(_hurtAnimatorKey))
        {
            if (_jump && Time.time - _lastPushTime > 0.2f)
            {

                _animator.SetBool(_hurtAnimatorKey, false);
            }

            _needToAttack = false;
            _needToCast = false;
            return;
        }
            _playerRB.velocity = new Vector2(_speed * _Hmove, _playerRB.velocity.y);

        if (CanClimb)
        {
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, _Vmove * _speed);
            _playerRB.gravityScale = 0;
        }
        else
        {
            _playerRB.gravityScale = 3;
        }     

        if(_Hmove != 0 && (_isGrounded || _airMove))
        {
            _playerRB.velocity = new Vector2(_speed * _Hmove, _playerRB.velocity.y); ;
        }
        
        if (_jump && _isGrounded)
        {
            _playerRB.AddForce(Vector2.up * _jumpForce);
            _jump = false;
        }


        if (_needToAttack)
        {
            StartToAttack();
            _Hmove = 0;
        }
        if (_needToCast)
        {
            StartToCast();
            _Hmove = 0;
        }

        _canStand = !Physics2D.OverlapCircle(_headChecker.position, _radius, _whatIsCell);

        _animator.SetBool(_jumpAnimatorKey, !_isGrounded);
        _animator.SetBool(_crouchAnimatorKey, !_headCollider.enabled);

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_headChecker.position, _radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_swordAttackPoint.position, new Vector3(_swordAttackRadius, _swordAttackRadius, 0));
    }
    void Flip()
    {
        _spriteFlip = !_spriteFlip;
        transform.Rotate(0f, 180f, 0f);
    }

    public void AddHP(int hpPoints)
    {
        int missingHp = _maxHp - CurrentHp;
        int pointToAdd = missingHp > hpPoints ? hpPoints : missingHp;
        StartCoroutine(RestoreHp(pointToAdd));
        
    }
    public void AddManna(int mannaPoints)
    {
        if (Fireball)
        {
            CurrentManna = _maxManna;
            int missingManna = _maxManna - CurrentManna;
            int mannaAdd = missingManna > mannaPoints ? mannaPoints : missingManna;
            StartCoroutine(RestoreHp(mannaAdd));
        }
    }
    private IEnumerator RestoreHp(int pointToAdd)
    {
        while (pointToAdd != 0)
        {
            pointToAdd--;
            CurrentHp++;
            yield return new WaitForSeconds(0.2f);
        }
    }
    private IEnumerator RestoreManna(int mannaAdd)
    {
        while (mannaAdd != 0)
        {
            mannaAdd--;
            CurrentManna++;
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void TakeDamage(int damage, float pushPower = 0, float enemyPosX = 0)
    {
        if (_animator.GetBool(_hurtAnimatorKey))
        {
            return;
        }

        CurrentHp -= damage;
        Debug.Log(CurrentHp);
        if(CurrentHp<= 0)
        {
            Debug.Log("Died!");
            gameObject.SetActive(false);
            Invoke(nameof(ReloadScene), 1f);
        }

        if(pushPower != 0)
        {
            _lastPushTime = Time.time;
            int direction = transform.position.x > enemyPosX ? 1 : -1;
            _playerRB.AddForce(new Vector2(direction * pushPower / 2, pushPower));
            _animator.SetBool(_hurtAnimatorKey, true);
        }
    }
    
    private void Attack()
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll(_swordAttackPoint.position, new Vector2(_swordAttackRadius, _swordAttackRadius), _whatIsEnemy);
        
        foreach (var target in targets)
        {
            Plant plant = target.GetComponent<Plant>();
            if (plant != null)
            {
                plant.TakeDamage(_swordDamage);
            }
        }

        _animator.SetBool(_attackAnimatorKey, false);
        _needToAttack = false;
    }
    private void StartToAttack()
    {
        if (_animator.GetBool(_attackAnimatorKey))
        {
            return;
        }
        _animator.SetBool(_attackAnimatorKey, true);
    }
    private void Cast()
    {
        if (Fireball)
        {
            RaycastHit2D[] hits =
              Physics2D.RaycastAll(_skillCastPoint.position, transform.right, _skillLength, _whatIsEnemy);
            foreach (var hit in hits)
            {
                Plant target = hit.collider.GetComponent<Plant>();
                if (target != null)
                {
                    target.TakeDamage(_skillDamage);
                }
            }
            _animator.SetBool(_castAnimatorKey, false);
            _castLine.SetPosition(0, _skillCastPoint.position);
            _castLine.SetPosition(1, _skillCastPoint.position + transform.right * _skillLength);
            _castLine.enabled = true;
            _needToCast = false;
            Invoke(nameof(DisableLine), 0.1f);
        }
        return;
    }
    private void DisableLine()
    {
        _castLine.enabled = false;
    }
    private void StartToCast()
    {
        if (_animator.GetBool(_castAnimatorKey))
        {
            return;
        }

        _animator.SetBool(_castAnimatorKey, true);
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
