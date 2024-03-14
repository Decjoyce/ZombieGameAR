using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Timed : GameState_Base
{
    float ogWaveTime = 30f;
    float waveTimeMultiplier = 1.5f;
    public override void EnterState(GameManager manager)
    {
        ogWaveTime = 30f;
        manager.waveTime = ogWaveTime;
        manager.zombieManager.StartSpawningZombies();
        manager.CallCoroutine(Delay(manager));
    }

    public override void ExitState(GameManager manager)
    {
        
    }

    public override void FrameUpdate(GameManager manager)
    {

    }

    public override void PhysicsUpdate(GameManager manager)
    {
        
    }

    public override void NextWave(GameManager manager)
    {
        if (manager.wave % 2 != 0)
        {
            Debug.Log(manager.wave % 3);
                ogWaveTime += 15;
        }

        manager.waveTime = ogWaveTime;
        manager.CallCoroutine(Delay(manager));
    }

    public IEnumerator Delay(GameManager manager)
    {
        yield return new WaitForSeconds(manager.waveTime);
        DebugTextDisplayer.instance.ChangeText("INTERMISSION");
        manager.NextWave();
    }

}
