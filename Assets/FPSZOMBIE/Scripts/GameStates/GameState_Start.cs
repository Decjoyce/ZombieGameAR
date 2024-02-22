using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Start : GameState_Base
{
    public override void EnterState(GameManager manager)
    {

    }

    public override void ExitState(GameManager manager)
    {
        manager.arPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
        manager.arPlaneManager.enabled = false;
    }
}
