using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float currentScore;
    public int currentCombo;
    [SerializeField] float multiplier;

    public void IncreaseScore(float amount)
    {
        currentScore += amount * ComboMultiplier();
    }

    float ComboMultiplier()
    {
        return currentCombo * multiplier; 
    }

    public void IncreaseCombo()
    {
        currentCombo++;
    }
}
