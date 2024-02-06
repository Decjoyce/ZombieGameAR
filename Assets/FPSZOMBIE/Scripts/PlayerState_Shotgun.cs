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
        if (currentAmmo <= 0 && Time.time >= reloadDelay)
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
            for (int i = 0; i < 7; i++)
            {
                ShootShotGun(manager);
            }
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

    void ShootShotGun(Player_FPS manager)
    {

        Vector3 horiSpread = Vector3.right * Random.Range(-0.75f, -0.75f);
        Vector3 vertSpread = Vector3.right * Random.Range(-0.75f, 0.75f);
        RaycastHit hit;
        if (Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward + horiSpread + vertSpread, out hit, manager.currentWeapon.range))
        {
            if (hit.transform.CompareTag("Zombie"))
            {
                Zombie_FPS hitZombie = hit.transform.GetComponent<Zombie_FPS>();
                hitZombie.zombieHealth.TakeDamage(manager.currentWeapon.damage);

            }
            DebugTextDisplayer.instance.ChangeText("Hit " + hit.transform.name);
            Debug.Log("Hit " + hit.transform.name);
        }
        Debug.DrawRay(manager.cam.transform.position, manager.cam.transform.forward + horiSpread + vertSpread, Color.red, 10f);
    }

}
