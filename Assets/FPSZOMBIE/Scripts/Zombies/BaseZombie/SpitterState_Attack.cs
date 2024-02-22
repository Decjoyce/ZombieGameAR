using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SpitterState_Attack : SpitterState_Base
{
    float attackDelay;
    bool canAttack = true;
    public override void EnterState(Zombie_Spitter manager)
    { 

    }

    public override void ExitState(Zombie_Spitter manager)
    {

    }

    public override void OnTriggerEnter(Zombie_Spitter manager, Collider other)
    {

    }

    public override void OnTriggerExit(Zombie_Spitter manager, Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.SwitchState("FOLLOW");
        }
    }

    public override void FrameUpdate(Zombie_Spitter manager)
    {
        if(Time.time >= attackDelay)
        {
            attackDelay = Time.time + 1f / manager.speed_attack;
            Throw(manager);
        }
    }

    public override void PhysicsUpdate(Zombie_Spitter manager)
    {
        Vector3 newRot = Quaternion.LookRotation(manager.player.transform.position - manager.transform.position, Vector3.up).eulerAngles;
        manager.transform.eulerAngles = new Vector3(0, newRot.y, 0);
    }

    void Attack(Zombie_Spitter manager)
    {
        manager.playerHealth.TakeDamage(manager.damage);
        DebugTextDisplayer.instance.ChangeText("Zombie Attack");
    }

    public void Throw(Zombie_Spitter manager)
    {
        GameObject projectile = manager.HelpInstantiate(manager.objectToThrow, manager.attackPoint.position, manager.attackPoint.rotation);
        
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = manager.transform.forward * manager.throwForce + manager.transform.up * manager.throwUpwardForce;

        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        manager.totalThrows--;
    }
}
