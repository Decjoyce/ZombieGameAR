using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zombie_Health : MonoBehaviour
{
    public int health;
    public int currentHealth;
    public float DeadSeconds;
    public int scoreAmount;

    public virtual void TakeDamage(int amount, bool addScore = true, bool headShot = false)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die(addScore);
        }
    }

    public virtual void Die(bool addScore = true, bool headShot = false)
    {
        if (addScore)
        {
            if(!headShot)
                ScoreManager.instance.IncreaseScore(scoreAmount);
            else
                ScoreManager.instance.IncreaseScore(scoreAmount * 2);
        }

        NewZombieManager.instance.ZombieDead(gameObject);
        Destroy(gameObject, DeadSeconds);
    }

}
