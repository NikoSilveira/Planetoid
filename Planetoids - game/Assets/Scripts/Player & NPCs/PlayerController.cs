using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    public Joystick joystick;
    private Vector3 moveDir;
    private float moveSpeed;
    private float horizontalInput;
    private float verticalInput;

    //Control
    private bool isAlive;

    //Flame
    private GameObject flameParticles;

    private void Start()
    {
        flameParticles = gameObject.transform.GetChild(1).gameObject;

        InitializeColors();
        
        isAlive = true;
        moveSpeed = 1.1f;
    }

    void Update()
    {
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
    //    METHODS
    //--------------

    public void PlayerDeath()
    {
        isAlive = false;

        //Animation effects
        LeanTween.alpha(gameObject, 0f, 0.25f);
        LeanTween.scale(flameParticles, new Vector3(1.5f, 1.5f, 1.5f), 0.2f);
        flameParticles.GetComponent<ParticleSystem>().Stop();
    }

    private void InitializeColors()
    {
        //read from file
        PlayerData data = SaveSystem.LoadPlayer();

        gameObject.GetComponent<MeshRenderer>().material.color = new Color(data.red, data.green, data.blue, 0.5f);

        ParticleSystem.MainModule settings = flameParticles.GetComponent<ParticleSystem>().main;
        settings.startColor = new ParticleSystem.MinMaxGradient(new Color(data.red, data.green, data.blue, 0.75f));
    }

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
