using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterState_Stagger : SpitterState_Base
{
    float staggerDelay;
    public override void EnterState(Zombie_Spitter manager)
    { 
            staggerDelay = Time.time + 1f / manager.staggerTime;
    }

    public override void ExitState(Zombie_Spitter manager)
    {

    }

    public override void OnTriggerEnter(Zombie_Spitter manager, Collider other)
    {

    }

    public override void OnTriggerExit(Zombie_Spitter manager, Collider other)
    {

    }

    public override void FrameUpdate(Zombie_Spitter manager)
    {
        if(Time.time >= staggerDelay)
        {
            manager.SwitchState("FOLLOW");
        }
    }

    public override void PhysicsUpdate(Zombie_Spitter manager)
    {

    }

}
