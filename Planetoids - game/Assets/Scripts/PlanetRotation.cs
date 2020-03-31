using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    void Start()
    {
        LeanTween.rotateAround(gameObject, Vector3.down, 360, 25f).setLoopClamp();
    }
}
