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

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        ScoreManager.instance.IncreaseScore(scoreAmount);
        ZombieManager.instance.ZombieDead(gameObject);
        Destroy(gameObject, DeadSeconds);
    }

}
