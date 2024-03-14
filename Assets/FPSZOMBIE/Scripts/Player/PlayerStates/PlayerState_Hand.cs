using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState_Hand : PlayerState_Base
{
    RaycastHit hit;

    public override void EnterState(Player_FPS manager)
    {
        manager.crosshairs.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }

    public override void ExitState(Player_FPS manager)
    {
        manager.crosshairs.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public override void FrameUpdate(Player_FPS manager)
    {
        if (Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("WeaponDrop"))
            {
                WeaponPickup weap = hit.transform.GetComponent<WeaponPickup>();
                // manager.currentWeapon = weap.weapon;
                // weap.PriceCheck();

                manager.costText.text = "- $" + weap.cost;
            }
        }
        else
            manager.costText.text = "";
    }

    public override void TouchInput(Player_FPS manager)
    {
        if (Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("WeaponDrop"))
            {
                WeaponPickup weap = hit.transform.GetComponent<WeaponPickup>();
                // manager.currentWeapon = weap.weapon;
                // weap.PriceCheck();

                if (weap.CheckPrice())
                {
                    manager.PickUpWeapon(weap.weapon);
                    weap.PickedUpWeapon();
                }
            }
        }

    }
}
