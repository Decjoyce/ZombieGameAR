using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState_Bazooka : PlayerState_Base
{
    float attackDelay;
    bool canShoot;
    float reloadSpeed;
    int currentAmmo;
    int reserveAmmo;

    public override void EnterState(Player_FPS manager)
    {
        currentAmmo = manager.currentWeapon.magCapacity;
        reserveAmmo = manager.currentWeapon.reserveAmmo;
        reloadSpeed = manager.currentWeapon.reloadSpeed;
        manager.text.text = reserveAmmo + "|" + currentAmmo;
    }

    public override void FrameUpdate(Player_FPS manager)
    {
        if (!canShoot && Time.time >= attackDelay)
        {
            canShoot = true;
        }
    }

    public override void TouchInput(Player_FPS manager)
    {
        DebugTextDisplayer.instance.ChangeText("Shot");

        if(canShoot && currentAmmo > 0)
        {
            ShootRocket(manager);

            currentAmmo--;
            manager.text.text = reserveAmmo + "|" + currentAmmo;

            if (currentAmmo > 0)
            {
                canShoot = false;
                attackDelay = Time.time + 1f / manager.currentWeapon.fireRate;
            }
            else
            {
                if (reserveAmmo == 0)
                {
                    DebugTextDisplayer.instance.ChangeText("Out of Ammo");
                    manager.ReturnToDefaultWeapon();
                }
                manager.OnReload();
            }
        }

    }

    void ShootRocket(Player_FPS manager)
    {

        RaycastHit hit;
        manager.audio.PlayOneShot(manager.clip);
        if (Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward, out hit, manager.currentWeapon.range))
        {
            manager.HelpInstantiate(manager.currentWeapon.impact, hit.point, Quaternion.Euler(Vector3.zero));
        }
        else
            manager.SpawnBulletTrail(manager.cam.transform.position + manager.cam.transform.forward * manager.currentWeapon.range);

    }

    public override IEnumerator Reload(Player_FPS manager)
    {
        DebugTextDisplayer.instance.ChangeText("Reloading");
        Handheld.Vibrate();
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = manager.currentWeapon.magCapacity;
        reserveAmmo--;
        DebugTextDisplayer.instance.ChangeText("Reloaded!");
        manager.text.text = reserveAmmo + "|" + currentAmmo;
    }

}
