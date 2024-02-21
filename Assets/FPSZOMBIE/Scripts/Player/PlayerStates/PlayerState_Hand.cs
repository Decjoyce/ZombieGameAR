using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState_Hand : PlayerState_Base
{
    RaycastHit hit;

    public override void EnterState(Player_FPS manager)
    {
        manager.text.gameObject.SetActive(false);
        DebugTextDisplayer.instance.ChangeText("Pickup Mode");
    }

    public override void ExitState(Player_FPS manager)
    {
        manager.text.gameObject.SetActive(true);
    }

    public override void TouchInput(Player_FPS manager)
    {
        DebugTextDisplayer.instance.ChangeText("Grab");
        if (Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("WeaponDrop"))
            {
                WeaponPickup weap = hit.transform.GetComponent<WeaponPickup>();
                // manager.currentWeapon = weap.weapon;
                // weap.PriceCheck();

                if (weap.canBuyShotgun == true)
                {
                    manager.currentWeapon = weap.weapon;
                    weap.PickedUpWeapon();
                }
                else return;
            }
            Debug.Log(hit.transform.name);
        }
    }
}
