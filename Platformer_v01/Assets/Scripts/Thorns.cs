using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private int _damage;
    
    private float _lastDamageTime;
    private void OnTriggerStay2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();
        if (player!= null && Time.time - _lastDamageTime > 0.04f)
        {
            _lastDamageTime = Time.time;
            player.TakeDamage(_damage);
        }
    }

}
