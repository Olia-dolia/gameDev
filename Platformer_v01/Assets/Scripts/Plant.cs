using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _shootAnimatorKey;
    [SerializeField] private bool _faceRight;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private int _maxHp;
    [SerializeField] private GameObject _enemySystem;
    private int _currentHp;

    private bool _canShoot;

    private void Start()
    {
        _hpBar.maxValue = _maxHp;
        ChangeHp(_maxHp);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(_attackRange * 2, 1, 0));
    }

    private void ChangeHp(int hp)
    {
        _currentHp = hp;
        if (_currentHp <= 0)
        {
            Destroy(_enemySystem);
        }
        _hpBar.value = hp;
    }

    private void FixedUpdate()
    {
        if (_canShoot)
        {
            return;
        }
        CheckIfCanShoot();
    }

    private void CheckIfCanShoot()
    {
        Collider2D player = Physics2D.OverlapBox(transform.position, new Vector2(_attackRange, 1), 0, _whatIsPlayer);
        if (player != null)
        {
            StartShoot(player.transform.position);
            _canShoot = true;
        }
        else
        {
            _canShoot = false;
        }
    }

    private void StartShoot(Vector2 position)
    {
        float posX = transform.position.x;
        if (posX < position.x && !_faceRight || posX > position.x && _faceRight)
        {
            transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }
        _animator.SetBool(_shootAnimatorKey, true);
    }

    public void Shoot()
    {
        Bullet bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity);
        bullet.StartFly(transform.right);
        _animator.SetBool(_shootAnimatorKey, false);
        Invoke(nameof(CheckIfCanShoot), 1f);
    }

    public void TakeDamage(int damage)
    {
        ChangeHp(_currentHp - damage);
    }
}
