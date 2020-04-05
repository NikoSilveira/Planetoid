using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player Controller Script
 * -Movement
 * -Boosters
 * -Player death
 */

public class PlayerController : MonoBehaviour
{
    //Movement
    public Joystick joystick;
    private Vector3 moveDir;
    private float moveSpeed;
    private float originalSpeed;
    private float horizontalInput;
    private float verticalInput;

    //Speed boost
    private float boostTimer;
    private bool boostEnd;

    //Control
    private bool isAlive;
    private bool isInvincible;
    private bool levelIsActive;
    public GameObject defeat;

    //Flame
    private GameObject flameParticles;

    private void Start()
    {
        flameParticles = gameObject.transform.GetChild(1).gameObject;

        isAlive = true;
        levelIsActive = true;

        moveSpeed = 1.1f;
        originalSpeed = moveSpeed;
        boostTimer = 0f;
        boostEnd = false;
    }

    void Update()
    {
        if (!isAlive)
        {
            levelIsActive = false;
        }

        //Speed boost
        if(boostTimer > 0f)
        {
            boostTimer = boostTimer - 1 * Time.deltaTime;
        }
        else if(boostEnd)
        {
            moveSpeed = originalSpeed;
            boostEnd = false;
        }

        //Movement
        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;

        moveDir = new Vector3(horizontalInput, 0, verticalInput).normalized;    //Input.GetAxisRaw("Vertical/Horizontal") for keyboard input
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + GetComponent<Transform>().TransformDirection(moveDir) * moveSpeed * Time.fixedDeltaTime);
            RotateFlame();
        }
    }

    //--------------
    //   BOOSTERS
    //--------------

    public void BoostSpeed()
    {
        moveSpeed = 1.5f;
        boostTimer = 6f;
        boostEnd = true;
    }

    public void MakeInvincible()
    {
        isInvincible = true;
    }

    //--------------
    //    EVENTS
    //--------------

    public void PlayerDeath()
    {
        if (isInvincible)
        {
            isInvincible = false;
        }
        else
        {
            isAlive = false;
            defeat.SetActive(true);

            //Animation effects
            LeanTween.alpha(gameObject, 0f, 0.25f);
            LeanTween.scale(flameParticles, new Vector3(1.5f,1.5f,1.5f), 0.2f);
            flameParticles.GetComponent<ParticleSystem>().Stop();

            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LevelLoader>().LoadTargetLevel(1);
    }

    //---------------------
    //  GETTERS & SETTERS
    //---------------------

    public bool GetIsAlive()
    {
        return isAlive;
    }

    public bool GetLevelIsActive()
    {
        return levelIsActive;
    }

    public void SetLevelIsActive(bool isActive)
    {
        levelIsActive = isActive;
    }

    //------------------
    //  FLAME ROTATION
    //------------------

    private void RotateFlame()
    {
        if (horizontalInput < 0.15f && verticalInput < 0.15f && verticalInput > -0.15f && horizontalInput > -0.15f)
        {
            //Idle flame
            LeanTween.rotateLocal(flameParticles, new Vector3(-90, 0, 0), 0.4f);
        }
        else
        {
            float flameAngle;

            flameAngle = Mathf.Atan(verticalInput / horizontalInput);
            flameAngle = (flameAngle * 180) / Mathf.PI;

            if(verticalInput > 0 && horizontalInput > 0)       
            {
                flameAngle = -(flameAngle + 90);  //1st quadrant
            }
            else if(verticalInput > 0 && horizontalInput < 0)  
            {
                flameAngle = -(flameAngle + 270); //2nd quadrant
            }
            else if(verticalInput < 0 && horizontalInput < 0)  
            {
                flameAngle = -(flameAngle + 270); //3rd quadrant
            }
            else if(verticalInput < 0 && horizontalInput > 0)  
            {
                flameAngle = -(flameAngle + 90);  //4th quadrant
            }

            flameParticles.transform.localEulerAngles = new Vector3(0, flameAngle, 0);
        }
    }

}
