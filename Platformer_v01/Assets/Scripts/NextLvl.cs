using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLvl : MonoBehaviour
{
    [SerializeField] private GameObject _pointer;
    [SerializeField] private int _levelToLaod;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();
        if (player != null) 
        { 
            if(player.ActivateAltar == true)
            {
                _pointer.SetActive(true);
                Debug.Log("Go to nextlvl");
           }
        }
    }
}
