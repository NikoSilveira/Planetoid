using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player Controller Script
 */

public class PlayerController : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    private Vector3 moveDir;

    //Position

    private void Start()
    {
        //Adjust value to modify player speed
        moveSpeed = 1.25f;
    }

    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

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
