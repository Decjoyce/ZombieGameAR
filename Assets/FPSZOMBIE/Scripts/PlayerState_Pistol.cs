using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Pistol : PlayerState_Base
{
    float attackDelay;
    bool canShoot;
    float reloadDelay;
    int currentAmmo;
    RaycastHit hit;


    public override void EnterState(Player_FPS manager)
    {
        currentAmmo = manager.currentWeapon.magCapacity;
    }

    public override void FrameUpdate(Player_FPS manager)
    {
        if (!canShoot && Time.time >= attackDelay)
        {
            canShoot = true;
            DebugTextDisplayer.instance.ChangeText("Can Shoot!");
        }
        if(currentAmmo <= 0 && Time.time >= reloadDelay)
        {
            currentAmmo = manager.currentWeapon.magCapacity;
            DebugTextDisplayer.instance.ChangeText("Reloaded!");
        }
        manager.text.text = currentAmmo + "/" + manager.currentWeapon.magCapacity;
    }

    public override void TouchInput(Player_FPS manager)
    {
        DebugTextDisplayer.instance.ChangeText("Shot");
        if(canShoot && currentAmmo > 0)
        {
            if (Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward, out hit, manager.currentWeapon.range))
            {
                if (hit.transform.CompareTag("Zombie"))
                {
                    Zombie_FPS hitZombie = hit.transform.GetComponent<Zombie_FPS>();
                    hitZombie.zombieHealth.TakeDamage(manager.currentWeapon.damage);

                    DebugTextDisplayer.instance.ChangeText("Hit Zombie");
                }
                Debug.Log("HIT");
                manager.SpawnBulletTrail(hit.point);
            }
            else
                manager.SpawnBulletTrail(manager.cam.transform.position + manager.cam.transform.forward * manager.currentWeapon.range);
            currentAmmo--;
            if (currentAmmo > 0)
            {
                canShoot = false;
                attackDelay = Time.time + 1f / manager.currentWeapon.fireRate;
            }
            else
            {
                DebugTextDisplayer.instance.ChangeText("Reloading");
                reloadDelay = Time.time + 1f / manager.currentWeapon.reloadSpeed;
            }
        }

    }
}
