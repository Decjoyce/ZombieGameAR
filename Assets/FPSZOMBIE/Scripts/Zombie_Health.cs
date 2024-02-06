using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Health : MonoBehaviour
{
    public int health;
    public int currentHealth;

    // Start is called before the first frame update
    void Awake()
    {
        health = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentHealth);

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
