using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Timed : GameState_Base
{
    float ogWaveTime = 30f;
    float waveTime;
    float waveTimeMultiplier = 1.5f;
    public override void EnterState(GameManager manager)
    {
        waveTime = ogWaveTime;
        manager.waveManager.StartSpawningZombies();
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
        ogWaveTime *= waveTimeMultiplier;
        waveTime = ogWaveTime;
    }

    public IEnumerator Delay(GameManager manager)
    {
        yield return new WaitForSeconds(waveTime);
        DebugTextDisplayer.instance.ChangeText("INTERMISSION");
        manager.NextWave();
    }

}
