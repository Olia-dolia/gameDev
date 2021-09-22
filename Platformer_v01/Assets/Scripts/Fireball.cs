using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();

        if (player != null)
        {
            player.Fireball = true;
            Debug.Log("Now you can use Fireball!");
            Destroy(gameObject);
        }
    }
}
