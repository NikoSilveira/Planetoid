using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Main script for defining enemy behavior and conditions
 */
public class EnemyController : MonoBehaviour
{

    //Movement variables
    private Vector3 moveDir;
    private float moveSpeed;
    private float horizontalInput;
    private float verticalInput;

    //Timer
    private float pivotTime = 0f;

    //Flame
    private GameObject flameParticles;

    void Start()
    {
        moveSpeed = 0.8f;

        flameParticles = gameObject.transform.GetChild(0).gameObject;

        RandomHorizontalDir();
        RandomVerticalDir();
    }

    void Update()
    {
        //Random direction change (timed)
        pivotTime = pivotTime + 1 * Time.deltaTime;

        if (pivotTime >= 3)
        {
            pivotTime = 0f;
            RandomHorizontalDir();
        }

        moveDir = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + GetComponent<Transform>().TransformDirection(moveDir) * moveSpeed * Time.fixedDeltaTime);
        RotateFlame();
    }

    //-----------------
    //    COLLISION
    //-----------------

    //Enemy damages player
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" && FindObjectOfType<PlayerController>().GetLevelIsActive())//here
        {
            FindObjectOfType<PlayerController>().PlayerDeath();//here
        }

        RandomHorizontalDir();
        RandomVerticalDir();
    }

    //------------------
    //  FLAME ROTATION
    //------------------

    private void RotateFlame()
    {
        float flameAngle;

        flameAngle = Mathf.Atan(verticalInput / horizontalInput);
        flameAngle = (flameAngle * 180) / Mathf.PI;

        if (verticalInput > 0 && horizontalInput > 0)
        {
            flameAngle = -(flameAngle + 90);  //1st quadrant
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
            flameAngle = -(flameAngle + 90);  //4th quadrant
        }

        flameParticles.transform.localEulerAngles = new Vector3(0, flameAngle, 0);
    }

    //-----------------
    //   RANDOMIZERS
    //-----------------

    private void RandomHorizontalDir()
    {
        if (Random.Range(0, 2) == 0)
        {
            horizontalInput = Random.Range(0.75f, 1f);
        }
        else
        {
            horizontalInput = Random.Range(-1f, -0.75f);
        }
    }

    private void RandomVerticalDir()
    {
        if (Random.Range(0, 2) == 0)
        {
            verticalInput = Random.Range(0.75f, 1f);
        }
        else
        {
            verticalInput = Random.Range(-1f, -0.75f);
        }
    }

}
