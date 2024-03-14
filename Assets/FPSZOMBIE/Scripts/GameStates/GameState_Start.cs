using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static Unity.VisualScripting.Member;

public class GameState_Start : GameState_Base
{
    public override void EnterState(GameManager manager)
    {
        manager.startButton.SetActive(false);
        manager.source.Play();
        //manager.arPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
        //manager.arPlaneManager.enabled = false;
    }

    public override void ExitState(GameManager manager)
    {
        //manager.arPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
        //manager.arPlaneManager.enabled = false;
        manager.source.Stop();
    }

    public override void FrameUpdate(GameManager manager)
    {
        if (manager.xrOrigin.transform.GetChild(1).childCount > 0)
        {
            if (manager.checkArPlaneSizes())            
                manager.startButton.SetActive(true);
        }

    }
}
