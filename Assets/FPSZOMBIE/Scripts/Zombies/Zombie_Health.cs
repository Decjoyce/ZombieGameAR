using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Health : MonoBehaviour
{
    Zombie_FPS zombie;
    public int health;
    public int currentHealth;
    public float DeadSeconds;

    //Hitboxing
    [SerializeField] Collider tursoCol;
    [SerializeField] Collider headCol;
    [SerializeField] Transform tursoPos;
    [SerializeField] Transform headPos;

    // Start is called before the first frame update
    void Awake()
    {
        health = currentHealth;
        zombie = GetComponent<Zombie_FPS>();
    }

    // Update is called once per frame
    private void Update()
    {
        tursoCol.transform.position = tursoPos.position;
        headCol.transform.position = headPos.position;
    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        zombie.SwitchState("STAGGER");

        if (currentHealth <= 0)
        {
            zombie.HandleDrops();
            zombie.anim.SetBool("IsDead", true);
            zombie.SwitchState("DEAD");
            tursoCol.enabled = false;
            headCol.enabled = false;
            ZombieManager.instance.ZombieDead(gameObject);
            Destroy(gameObject, DeadSeconds);
        }
    }

}
