using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rank
{
    [SerializeField]
    string letter;
    [SerializeField]
    int threshold;
    [SerializeField]
    Color rankColor;

    public string GetLetter()
    {
        return letter;
    }

    public int GetThreshold()
    {
        return threshold;
    }

    public Color GetRankColor()
    {
        return rankColor;
    }
}
