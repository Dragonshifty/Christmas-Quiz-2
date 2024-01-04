using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeep 
{
    int score;
    [SerializeField] int questionNumber = 10;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }


    public void ResetScore()
    {
        score = 0;
    }

    public int PercentageScore()
    {
        return Mathf.RoundToInt(score / (float)questionNumber * 100);
    }
}
