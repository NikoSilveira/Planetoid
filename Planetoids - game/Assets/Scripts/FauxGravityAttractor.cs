﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Add this script to the planet that generates attraction
 */

public class FauxGravityAttractor : MonoBehaviour
{

    public float gravity = -100;

    public void Attract(Transform body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.fixedDeltaTime);
    }
}
