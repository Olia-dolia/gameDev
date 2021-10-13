using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAltar : MonoBehaviour
{
    [SerializeField] private GameObject _fire2;
 
    private void Start()
    {
        _fire2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();
        
        if (player != null && player.Fireball == true)
        {
            player.ActivateAltar = true;
            _fire2.SetActive(true);
            player.Fireball = false;
            Debug.Log("ACTIVATED!!!");
        }
    }
}
