using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int _hpPoints;
    [SerializeField] private bool _hasFire;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();

        if (player != null)
        {
            player.AddHP(_hpPoints);
            Destroy(gameObject);
        }
    }
}
