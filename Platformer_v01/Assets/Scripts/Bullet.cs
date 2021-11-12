using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody;

    public void StartFly(Vector2 direction)
    {
        _rigidbody.velocity = direction * _speed;
        Invoke(nameof(Destroy), 5f);
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Destroy), _lifeTime);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();
        if(player != null)
        {
            player.TakeDamage(_damage);
        }
        Destroy();
    }
    

}
