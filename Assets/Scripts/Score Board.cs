using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score;
    public void increaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        Debug.Log("score is now " + score);
    }
}
