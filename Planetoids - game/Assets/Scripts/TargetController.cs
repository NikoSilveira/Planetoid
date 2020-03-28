using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Main script for defining target behavior and conditions
 */
public class TargetController : MonoBehaviour
{
    //Spawn force variables
    public float upForce = 1f;
    public float sideForce = .2f;

    //Movement variables
    private Vector3 moveDir;
    public float moveSpeed;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        //Spawn force
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;

        //Movement
        moveSpeed = 0.8f;
    }

    void Update()
    {
        horizontalInput = Random.Range(0.45f,0.6f);
        verticalInput = Random.Range(0.45f,0.6f);
        moveDir = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + GetComponent<Transform>().TransformDirection(moveDir) * moveSpeed * Time.fixedDeltaTime);
    }

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
