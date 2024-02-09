using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState_Stagger : ZombieState_Base
{
    float staggerDelay;
    public override void EnterState(Zombie_FPS manager)
    { 
            staggerDelay = Time.time + 1f / manager.staggerTime;
    }

    public override void ExitState(Zombie_FPS manager)
    {

    }

    public override void OnTriggerEnter(Zombie_FPS manager, Collider other)
    {

    }

    public override void OnTriggerExit(Zombie_FPS manager, Collider other)
    {

    }

    public override void FrameUpdate(Zombie_FPS manager)
    {
        if(Time.time >= staggerDelay)
        {
            manager.SwitchState("FOLLOW");
        }
    }

    public override void PhysicsUpdate(Zombie_FPS manager)
    {

    }

}
