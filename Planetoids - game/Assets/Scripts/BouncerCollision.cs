using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<Score>().setScore(10);
    }
}
