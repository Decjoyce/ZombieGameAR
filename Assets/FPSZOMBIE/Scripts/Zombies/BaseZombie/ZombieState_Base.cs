using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieState_Base
{
    public virtual void EnterState(Zombie_FPS manager)
    {

    }


    public virtual void ExitState(Zombie_FPS manager)
    {

    }

    public virtual void OnTriggerEnter(Zombie_FPS manager, Collider other)
    {

    }

    public virtual void OnTriggerExit(Zombie_FPS manager, Collider other)
    {

    }

    public virtual void FrameUpdate(Zombie_FPS manager)
    {

    }

    public virtual void PhysicsUpdate(Zombie_FPS manager)
    {

    }
}
