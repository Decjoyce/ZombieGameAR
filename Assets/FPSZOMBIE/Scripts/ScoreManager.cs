using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float currentScore;
    public int currentCombo;
    [SerializeField] float multiplier;

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

    public void IncreaseScore(float amount)
    {
        currentScore += amount;
    }

    public void ShotgunBought()
    {
        currentScore -= 15;
    }
}
