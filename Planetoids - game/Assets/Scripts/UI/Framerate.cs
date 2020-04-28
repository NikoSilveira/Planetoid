using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Framerate : MonoBehaviour
{
    public Text framerate;

    void Start()
    {
        framerate = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        framerate.text = (1.0f / Time.deltaTime).ToString("0");
    }
}
