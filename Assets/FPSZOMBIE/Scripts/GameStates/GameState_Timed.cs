using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Timed : GameState_Base
{
    float ogWaveTime = 30;
    float waveTime;
    float waveTimeMultiplier = 1.5f;
    bool waveOver;
    public override void EnterState(GameManager manager)
    {
        waveTime = ogWaveTime;
        manager.waveManager.StartSpawningZombies();
        Debug.Log("WTF");
    }

    public override void ExitState(GameManager manager)
    {

    }

    public override void FrameUpdate(GameManager manager)
    {

        manager.timerText.text = waveTime.ToString("0");

        if(!waveOver && waveTime <= 0)
        {
            manager.NextWave();
            waveOver = true;
        }
        else if(!waveOver)
            waveTime -= Time.deltaTime;
    }

    public override void PhysicsUpdate(GameManager manager)
    {
        
    }

    public override void NextWave(GameManager manager)
    {
        ogWaveTime *= waveTimeMultiplier;
        waveTime = ogWaveTime;
        waveOver = false;
    }

}
