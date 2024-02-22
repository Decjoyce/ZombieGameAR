using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState_Follow : ZombieState_Base
{

    public override void EnterState(Zombie_FPS manager)
    {
        
    }

    public override void ExitState(Zombie_FPS manager)
    {

    }

    public override void OnTriggerEnter(Zombie_FPS manager, Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.SwitchState("ATTACK");
        }
    }

    public override void OnTriggerExit(Zombie_FPS manager, Collider other)
    {

    }

    public override void FrameUpdate(Zombie_FPS manager)
    {

    }

    public override void PhysicsUpdate(Zombie_FPS manager)
    {
        float xpos = Mathf.Lerp(manager.transform.position.x, manager.player.transform.position.x, manager.speed_movement * Time.fixedDeltaTime);
        float zpos = Mathf.Lerp(manager.transform.position.z, manager.player.transform.position.z, manager.speed_movement * Time.fixedDeltaTime);
        manager.transform.position = new Vector3(xpos, manager.transform.position.y, zpos);

        Vector3 newRot = Quaternion.LookRotation(manager.player.transform.position - manager.transform.position, Vector3.up).eulerAngles;
        manager.transform.eulerAngles = new Vector3(0, newRot.y, 0);
    }

}
