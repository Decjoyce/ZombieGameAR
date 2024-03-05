using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameState_Start : GameState_Base
{
    public override void EnterState(GameManager manager)
    {
        manager.startButton.SetActive(false);
        manager.arPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
        manager.arPlaneManager.enabled = false;
    }

    public override void ExitState(GameManager manager)
    {
        manager.arPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
        manager.arPlaneManager.enabled = false;
    }

    public override void FrameUpdate(GameManager manager)
    {
        if (manager.xrOrigin.transform.GetChild(1).childCount > 0)
            manager.startButton.SetActive(true);
    }
}
