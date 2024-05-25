using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerScore
{
    public string playerName { get; set;}
    public int time { get; set;}

    public PlayerScore()
    {
        
    }

    public PlayerScore(string playerName, int time)
    {
        this.playerName = playerName;
        this.time = time;
    }
}
