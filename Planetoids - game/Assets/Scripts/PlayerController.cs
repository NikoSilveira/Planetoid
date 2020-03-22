using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player Controller Script
 * -Movement
 */

public class PlayerController : MonoBehaviour
{
    //Movement
    public Joystick joystick;
    private Vector3 moveDir;
    public float moveSpeed;
    private float horizontalInput;
    private float VerticalInput;


    private void Start()
    {
        //Adjust value to modify player speed
        moveSpeed = 1.25f;
    }

    void Update()
    {
        //Joystick deadzone (horizontal)
        if(joystick.Horizontal >= 0.15f || joystick.Horizontal <= -0.15f)
        {
            horizontalInput = joystick.Horizontal;
        }
        else
        {
            horizontalInput = 0f;
        }

        //Joystick deadzone (vertical)
        if (joystick.Vertical >= 0.15f || joystick.Vertical <= -0.15f)
        {
            VerticalInput = joystick.Vertical;
        }
        else
        {
            VerticalInput = 0f;
        }

        moveDir = new Vector3(horizontalInput, 0, VerticalInput).normalized;    //Input.GetAxisRaw("Vertical/Horizontal") for keyboard input

        if (Input.GetKeyDown("space"))
        {
            //For testing
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + GetComponent<Transform>().TransformDirection(moveDir) * moveSpeed * Time.fixedDeltaTime);
    }

}
