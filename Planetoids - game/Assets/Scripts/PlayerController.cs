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
    public float moveSpeed;
    public float originalSpeed;
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

    private void Start()
    {
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
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + GetComponent<Transform>().TransformDirection(moveDir) * moveSpeed * Time.fixedDeltaTime);
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
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LevelLoader>().LoadTargetLevel(0);
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

}
