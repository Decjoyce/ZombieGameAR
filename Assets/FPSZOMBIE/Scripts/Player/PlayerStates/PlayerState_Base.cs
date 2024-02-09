using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState_Base
{
    public virtual void EnterState(Player_FPS manager)
    {

    }


    public virtual void ExitState(Player_FPS manager)
    {

    }

    public virtual void OnTriggerEnter(Player_FPS manager, Collider other)
    {

    }

    public virtual void OnTriggerExit(Player_FPS manager, Collider other)
    {

    }

    public virtual void FrameUpdate(Player_FPS manager)
    {

    }

    public virtual void PhysicsUpdate(Player_FPS manager)
    {

    }

    public virtual void TouchInput(Player_FPS manager)
    {

    }
}
