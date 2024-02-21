using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpitterState_Base
{
    public virtual void EnterState(Zombie_Spitter manager)
    {

    }


    public virtual void ExitState(Zombie_Spitter manager)
    {

    }

    public virtual void OnTriggerEnter(Zombie_Spitter manager, Collider other)
    {

    }

    public virtual void OnTriggerExit(Zombie_Spitter manager, Collider other)
    {

    }

    public virtual void FrameUpdate(Zombie_Spitter manager)
    {

    }

    public virtual void PhysicsUpdate(Zombie_Spitter manager)
    {

    }
}
