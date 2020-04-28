﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int worldsToUnlock;
    public int levelsToUnlock;
    public int colorsToUnlock;

    public List<int> highScore = new List<int>();

    public GameData(LevelManager levelManager)
    {
        worldsToUnlock = levelManager.worldsToUnlock;
        levelsToUnlock = levelManager.levelsToUnlock;
        colorsToUnlock = levelManager.colorsToUnlock;

        highScore = levelManager.highScore;
    }
}
