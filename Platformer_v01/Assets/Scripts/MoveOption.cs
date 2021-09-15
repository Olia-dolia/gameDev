using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOption : MonoBehaviour
{
    public Rigidbody2D _player;
    public float _speedX;
    public float _horizontalSpeed;
    public Vector2 movement;


    private void Awake()
    {
        _player = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* for Option 5
         * movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"));
         */
    }

    private void FixedUpdate()
    {
        /* Option 1
         * 
         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _speedX = -_horizontalSpeed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _speedX = _horizontalSpeed;
        }
        transform.Translate(_speedX, 0, 0);
        _speedX = 0;
        */
        /*Option 2
         * 
         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _player.AddForce(new Vector2(-_speedX,0), ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _player.AddForce(new Vector2(_speedX, 0), ForceMode2D.Force);
        }
         */
        /*Option 3
         * 
          if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * (Time.deltaTime * _speedX);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * (Time.deltaTime * _speedX);
        }
         */
        /*Option 4
         * 
         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left  * (Time.deltaTime * _speedX));
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * (Time.deltaTime * _speedX));
        }
         */
        /*Option 5
         * 
            moveCharacter(movement);
        
         */
    }
    /*for Option 5
     * void moveCharacter(Vector2 direction)
    {
        _player.MovePosition((Vector2)transform.position + (direction * _speedX * Time.deltaTime));
    }*/

}
