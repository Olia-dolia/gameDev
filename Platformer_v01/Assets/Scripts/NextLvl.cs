using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvl : MonoBehaviour
{
    [SerializeField] private GameObject _pointer;
    [SerializeField] Collider2D _pointerCollider;
    [SerializeField] private bool _spriteFlip;
    [SerializeField] private int _levelToLoad;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();
       if(player != null && player.ActivateAltar == true)
       {              
            Flip();
            Debug.Log("Go to nextlvl");
            Invoke(nameof(_LoadScene), 1f);
       }
    }
    void Flip()
    {
        _pointerCollider.enabled = false;
        transform.Rotate(0f, 180f, 0f);
    }
    private void _LoadScene()
    {
        SceneManager.LoadScene(_levelToLoad);
    }
}
