using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Main script for defining target behavior and conditions
 */
public class TargetController : MonoBehaviour
{

    //Movement variables
    private Vector3 moveDir;
    public float moveSpeed;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        moveSpeed = 0.6f;
        
        //Random horizontal direction
        if(Random.Range(0,2) == 0)
        {
            horizontalInput = Random.Range(0.75f,1f);
        }
        else
        {
            horizontalInput = Random.Range(-1f,-0.75f);
        }

        //Random vertical direction
        if(Random.Range(0,2) == 0)
        {
            verticalInput = Random.Range(0.75f, 1f);
        }
        else
        {
            verticalInput = Random.Range(-1f, -0.75f);
        }
    }

    void Update()
    {
        moveDir = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + GetComponent<Transform>().TransformDirection(moveDir) * moveSpeed * Time.fixedDeltaTime);
    }

    //Player - target collisison
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            FindObjectOfType<Score>().setScore(10);
            FindObjectOfType<Counter>().setCounter();
            Destroy(gameObject);
        }
        
    }

}
