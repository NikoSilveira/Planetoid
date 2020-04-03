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

    //Timer
    private float pivotTime = 0f;

    //Flame
    private GameObject flameParticles;

    void Start()
    {
        moveSpeed = 0.6f;

        flameParticles = gameObject.transform.GetChild(0).gameObject;

        //Random horizontal direction
        if (Random.Range(0,2) == 0)
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
        //Random direction change
        pivotTime = pivotTime + 1 * Time.deltaTime;

        if(pivotTime >= 4)
        {
            pivotTime = 0f;

            //New random horizontal direction
            if (Random.Range(0, 2) == 0)
            {
                horizontalInput = Random.Range(0.75f, 1f);
            }
            else
            {
                horizontalInput = Random.Range(-1f, -0.75f);
            }
        }

        //Constant input
        moveDir = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + GetComponent<Transform>().TransformDirection(moveDir) * moveSpeed * Time.fixedDeltaTime);
        RotateFlame();
    }

    //Player - target collisison (eat target)
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" && FindObjectOfType<PlayerController>().GetLevelIsActive())
        {
            FindObjectOfType<Score>().SetScore(10, true);
            FindObjectOfType<Counter>().setCounter();
            Destroy(gameObject);
        }
    }


    //------------------
    //  FLAME ROTATION
    //------------------

    private void RotateFlame()
    {
        if (horizontalInput < 0.15f && verticalInput < 0.15f && verticalInput > -0.15f && horizontalInput > -0.15f)
        {
            //Idle flame
            //flameParticles.transform.localEulerAngles = new Vector3(-90, 0, 0);
            LeanTween.rotateLocal(flameParticles, new Vector3(-90, 0, 0), 0.4f);
        }
        else
        {
            float flameAngle;

            flameAngle = Mathf.Atan(verticalInput / horizontalInput);
            flameAngle = (flameAngle * 180) / Mathf.PI;

            if (verticalInput > 0 && horizontalInput > 0)
            {
                flameAngle = -(flameAngle + 90); //1st quadrant
            }
            else if (verticalInput > 0 && horizontalInput < 0)
            {
                flameAngle = -(flameAngle + 270); //2nd quadrant
            }
            else if (verticalInput < 0 && horizontalInput < 0)
            {
                flameAngle = -(flameAngle + 270); //3rd quadrant
            }
            else if (verticalInput < 0 && horizontalInput > 0)
            {
                flameAngle = -(flameAngle + 90); //4th quadrant
            }

            flameParticles.transform.localEulerAngles = new Vector3(0, flameAngle, 0);
            //LeanTween.rotateLocal(flameParticles, new Vector3(0,flameAngle,0), 0.25f);
        }
    }

}
