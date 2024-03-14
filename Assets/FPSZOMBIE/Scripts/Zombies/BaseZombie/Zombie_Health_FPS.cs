using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Health_FPS : Zombie_Health
{
    public Zombie_FPS zombie;

    //Hitboxing
    [SerializeField] Collider tursoCol;
    [SerializeField] Collider headCol;
    [SerializeField] Transform tursoPos;
    [SerializeField] Transform headPos;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    private void Update()
    {
        tursoCol.transform.position = tursoPos.position;
        headCol.transform.position = headPos.position;
    }

    public override void TakeDamage(int amount, bool addScore = true, bool headShot = false)
    {
        zombie.SwitchState("STAGGER");
        base.TakeDamage(amount);
    }

    public override void Die(bool addScore = true, bool headShot = false)
    {
        zombie.anim.SetBool("IsDead", true);
        zombie.SwitchState("DEAD");
        tursoCol.enabled = false;
        headCol.enabled = false;

        zombie.DropSomething();

        base.Die();
    }
}
