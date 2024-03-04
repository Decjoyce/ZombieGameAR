using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore;

    public static ScoreManager instance;

    [SerializeField] TextMeshProUGUI scoreTextInterRound, scoreTextGameplay;

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
        scoreTextGameplay.text = "$" + currentScore;
        scoreTextInterRound.text = "$" + currentScore;
    }

    public void DecreaseScore(int amount)
    {
        currentScore -= amount;
        scoreTextGameplay.text = "$" + currentScore;
        scoreTextInterRound.text = "$" + currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
