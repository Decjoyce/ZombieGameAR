using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Health : MonoBehaviour
{
    Zombie_FPS zombie;
    public int health;
    public int currentHealth;

    // Start is called before the first frame update
    void Awake()
    {
        health = currentHealth;
        zombie = GetComponent<Zombie_FPS>();
    }

    // Update is called once per frame

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        zombie.SwitchState("STAGGER");

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
