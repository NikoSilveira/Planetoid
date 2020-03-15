using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player Controller Script
 */

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private Vector3 moveDir;

    private void Start()
    {
        //Adjust value to modify player speed
        moveSpeed = 1.25f;
    }

    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + GetComponent<Transform>().TransformDirection(moveDir) * moveSpeed * Time.fixedDeltaTime);
    }
}
