using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore;

    public static ScoreManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of ScoreManager Found");
            return;
        }
        instance = this;
    }

    public void IncreaseScore(int amount)
    {
        currentScore += amount;
    }

    public void DecreaseScore(int amount)
    {
        currentScore -= amount;
    }
}
