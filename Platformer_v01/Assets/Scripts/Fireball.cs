using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private int _mannaPoints;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();

        if (player != null)
        {
            player.Fireball = true;
            player.AddManna(_mannaPoints);
            Debug.Log("Now you have Fireball!");
            Destroy(gameObject);
        }   

    }
}
