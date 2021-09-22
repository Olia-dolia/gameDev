using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();

        if (player != null)
        {
            player.Shield = true;
            Debug.Log("Now you can use Shield for 15 sec");
            Destroy(gameObject);
        }
    }
}
