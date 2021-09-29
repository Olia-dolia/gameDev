using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();
        if(player != null)
        {
            player.CanClimb = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();
        if (player != null)
        {
            player.CanClimb = false;
        }
    }
}
