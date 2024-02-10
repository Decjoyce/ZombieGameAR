using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState_Shotgun : PlayerState_Base
{
    float attackDelay;
    bool canShoot;
    float reloadDelay;
    int currentAmmo;
    int reserveAmmo;
    bool emptyGun;

    public override void EnterState(Player_FPS manager)
    {
        currentAmmo = manager.currentWeapon.magCapacity;
        reserveAmmo = manager.currentWeapon.reserveAmmo;
    }

    public override void FrameUpdate(Player_FPS manager)
    {
        if (!canShoot && Time.time >= attackDelay)
        {
            canShoot = true;
        }
        if (currentAmmo <= 0 && Time.time >= reloadDelay)
        {
            reserveAmmo--;
            currentAmmo = manager.currentWeapon.magCapacity;
            DebugTextDisplayer.instance.ChangeText("Reloaded!");
        }
        manager.text.text = reserveAmmo + "||" + currentAmmo;
    }

    public override void TouchInput(Player_FPS manager)
    {
        DebugTextDisplayer.instance.ChangeText("Shot");

        if(canShoot && currentAmmo > 0)
        {
            ShootShotGun(manager);
            currentAmmo--;
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
                DebugTextDisplayer.instance.ChangeText("Reloading");
                reloadDelay = Time.time + 1f / manager.currentWeapon.reloadSpeed;
            }
        }

    }

    void ShootShotGun(Player_FPS manager)
    {
        int zombieHits = 0;            
        RaycastHit hit;
        Zombie_FPS hitZombie = null;
        for (int i = 0; i < 7; i++)
        {
            Vector3 horiSpread = Vector3.right * Random.Range(-0.7f, 0.7f);
            Vector3 vertSpread = Vector3.up * Random.Range(-0.7f, 0.7f);

            if (Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward + (horiSpread + vertSpread), out hit, manager.currentWeapon.range))
            {
                if (hit.transform.CompareTag("Zombie/TURSO") || hit.transform.CompareTag("Zombie/Head"))
                {
                    hitZombie = hit.transform.GetComponent<Zombie_FPS>();
                    zombieHits++;
                    Debug.Log("Hit " + hit.transform.name);
                }
                DebugTextDisplayer.instance.ChangeText("Hit " + hit.transform.name);
                manager.SpawnBulletTrail(hit.point);
            }
            else
                manager.SpawnBulletTrail(manager.cam.transform.position + manager.cam.transform.forward * manager.currentWeapon.range + (horiSpread + vertSpread));
        }
        if(zombieHits > 0)
        {
            hitZombie.zombieHealth.TakeDamage(manager.currentWeapon.damage * zombieHits);
        }

    }

}
