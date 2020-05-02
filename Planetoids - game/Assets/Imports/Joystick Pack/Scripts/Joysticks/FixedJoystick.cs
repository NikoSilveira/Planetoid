using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixedJoystick : Joystick
{
    private float joystickOffset = 104f;
    private float timerOffset = 445;

    

    //-------------
    //   METHODS
    //-------------

    public void ChangeJoystickSide(Text joystickText)
    {
        int sideValue = PlayerPrefs.GetInt("JoystickSide", 0); //0-Right, 1-Left

        RectTransform joystick = FindObjectOfType<Joystick>().GetComponent<RectTransform>();
        RectTransform timer = FindObjectOfType<Timer>().GetComponent<RectTransform>();

        if (sideValue == 0)         //Move to the left
        {
            PlayerPrefs.SetInt("JoystickSide", 1);
            joystickText.text = "Joystick: L";

            timer.transform.localPosition = new Vector2(timerOffset, 0);
            joystick.anchorMin = new Vector2(0, 0);
            joystick.anchorMax = new Vector2(0, 0);
            joystick.transform.position = new Vector2(joystickOffset, joystickOffset);
        }
        else if (sideValue == 1)    //Move to the right
        {
            PlayerPrefs.SetInt("JoystickSide", 0);
            joystickText.text = "Joystick: R";

            timer.transform.localPosition = new Vector2(-timerOffset, 0);
            joystick.transform.position = new Vector2(-joystickOffset, joystickOffset);
            joystick.anchorMin = new Vector2(1, 0);
            joystick.anchorMax = new Vector2(1, 0);
        }
    }

    public void InitializeJoystick(Text joystickText)
    {
        RectTransform joystick = FindObjectOfType<Joystick>().GetComponent<RectTransform>();
        RectTransform timer = FindObjectOfType<Timer>().GetComponent<RectTransform>();

        //Initialize on left
        if (PlayerPrefs.GetInt("JoystickSide", 0) == 1)
        {
            PlayerPrefs.SetInt("JoystickSide", 1);
            joystickText.text = "Joystick: L";

            joystick.anchorMin = new Vector2(0, 0);
            joystick.anchorMax = new Vector2(0, 0);
            joystick.transform.position = new Vector2(joystickOffset, joystickOffset);
            timer.transform.localPosition = new Vector2(timerOffset, 0);
        }
    }
}