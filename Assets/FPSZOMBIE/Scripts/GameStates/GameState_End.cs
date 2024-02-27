using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_End : GameState_Base
{
    public override void EnterState(GameManager manager)
    {
        manager.OnGameOver.Invoke();
        manager.zombieManager.StopSpawningZombies();
        manager.StopCurrentCoroutine();
    }

    public override void ExitState(GameManager manager)
    {

    }
}
