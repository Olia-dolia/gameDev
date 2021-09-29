using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoins : MonoBehaviour
{
    [SerializeField] private int _coin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement_controller player = other.GetComponent<Movement_controller>();
        if (player != null)
        {
            player.CoinsAmount += +_coin;
            Debug.Log(_coin);
            Destroy(gameObject);
        }
    }
}
