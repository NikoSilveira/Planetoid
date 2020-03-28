﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Main script for defining enemy behavior and conditions
 */
public class EnemyController : MonoBehaviour
{

    public float upForce = 1f;
    public float sideForce = .2f;

    void Start()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce/2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Debug.Log("Damage");
        }
    }

}
