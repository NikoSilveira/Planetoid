﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Add this script to the player and add the planet (attractor) through inspector
 */

public class FauxGravityBody : MonoBehaviour
{

    private FauxGravityAttractor attractor;
    private Transform myTransform;

    void Start()
    {
        attractor = GameObject.Find("Planet").GetComponent<FauxGravityAttractor>();

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;
        myTransform = transform;
    }

    void Update()
    {
        attractor.Attract(myTransform);
    }
}
