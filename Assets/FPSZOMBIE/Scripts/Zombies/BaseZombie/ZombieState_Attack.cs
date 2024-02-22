using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState_Attack : ZombieState_Base
{
    float attackDelay;
    public override void EnterState(Zombie_FPS manager)
    { 

    }

    public override void ExitState(Zombie_FPS manager)
    {

    }

    public override void OnTriggerEnter(Zombie_FPS manager, Collider other)
    {

    }

    public override void OnTriggerExit(Zombie_FPS manager, Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.SwitchState("FOLLOW");
        }
    }

    public override void FrameUpdate(Zombie_FPS manager)
    {
        if(Time.time >= attackDelay)
        {
            attackDelay = Time.time + 1f / manager.speed_attack;
            Attack(manager);
        }
    }

    public override void PhysicsUpdate(Zombie_FPS manager)
    {
        Vector3 newRot = Quaternion.LookRotation(manager.player.transform.position - manager.transform.position, Vector3.up).eulerAngles;
        manager.transform.eulerAngles = new Vector3(0, newRot.y, 0);
    }

    void Attack(Zombie_FPS manager)
    {
        manager.playerHealth.TakeDamage(manager.damage);
        DebugTextDisplayer.instance.ChangeText("Zombie Attack");
    }
}
