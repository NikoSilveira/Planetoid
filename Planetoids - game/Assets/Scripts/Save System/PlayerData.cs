using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Player color
    public float red, green, blue;

    public PlayerData(MainMenu mainMenu)
    {
        red = mainMenu.red;
        green = mainMenu.green;
        blue = mainMenu.blue;
    }
}
