using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState_Base
{
    public virtual void EnterState(GameManager manager)
    {

    }

    public virtual void ExitState(GameManager manager)
    {

    }

    public virtual void FrameUpdate(GameManager manager)
    {

    }

    public virtual void PhysicsUpdate(GameManager manager)
    {

    }

    public virtual void NextWave(GameManager manager)
    {

    }
}
