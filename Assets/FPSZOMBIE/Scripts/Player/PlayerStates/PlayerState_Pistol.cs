using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Pistol : PlayerState_Base
{
    // 
    // This is the default Pistol State
    // 


    float attackDelay;
    bool canShoot;
    float reloadSpeed;
    int currentAmmo;
    RaycastHit hit;



    public override void EnterState(Player_FPS manager)
    {
        currentAmmo = manager.currentWeapon.magCapacity;
        reloadSpeed = manager.currentWeapon.reloadSpeed;
        manager.text.text = currentAmmo + "/" + manager.currentWeapon.magCapacity;
    }

    public override void FrameUpdate(Player_FPS manager)
    {
        //This adds delay between shots (done in update because calling coroutines need to be done in a script derriving from Monobehaviour)
        if (!canShoot && Time.time >= attackDelay)
        {
            canShoot = true;
        }
        //Updates the text - this will be changed as it does not need to be called every frame
    }

    public override void TouchInput(Player_FPS manager)
    {
        DebugTextDisplayer.instance.ChangeText("Shot");
        if(canShoot && currentAmmo > 0)
        {
            manager.audio.PlayOneShot(manager.clip);
            if (Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward, out hit, manager.currentWeapon.range))
            {
                if (hit.transform.CompareTag("Zombie/Turso"))
                {
                    Zombie_Health zombieHealth = hit.transform.parent.GetComponent<Zombie_Health>();
                    zombieHealth.TakeDamage(manager.currentWeapon.damage);

                    DebugTextDisplayer.instance.ChangeText("Hit Zombie");
                }
                if (hit.transform.CompareTag("Zombie/Head"))
                {
                    Zombie_Health zombieHealth = hit.transform.parent.GetComponent<Zombie_Health>();
                    zombieHealth.TakeDamage(manager.currentWeapon.damage * 2, headShot: true);

                    DebugTextDisplayer.instance.ChangeText("HEADSHOT");
                }

                Debug.Log("HIT");
                manager.SpawnBulletTrail(hit.point);
            }
            else
                manager.SpawnBulletTrail(manager.cam.transform.position + manager.cam.transform.forward * manager.currentWeapon.range);

            currentAmmo--;
            manager.text.text = currentAmmo + "/" + manager.currentWeapon.magCapacity;
            if (currentAmmo > 0)
            {
                canShoot = false;
                attackDelay = Time.time + 1f / manager.currentWeapon.fireRate;
            }
            else
            {
                manager.OnReload();
            }
        }

    }

    public override IEnumerator Reload(Player_FPS manager)
    {
        DebugTextDisplayer.instance.ChangeText("Reloading");
        Handheld.Vibrate();
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = manager.currentWeapon.magCapacity;
        DebugTextDisplayer.instance.ChangeText("Reloaded!");
        manager.text.text = currentAmmo + "/" + manager.currentWeapon.magCapacity;
    }
}
