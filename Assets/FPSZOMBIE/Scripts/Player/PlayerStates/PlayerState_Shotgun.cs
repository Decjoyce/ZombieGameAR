using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState_Shotgun : PlayerState_Base
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
        //manager.text.text = reserveAmmo + "|" + currentAmmo;
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
            manager.audio.PlayOneShot(manager.clip);

            ShootShotGun(manager);

            currentAmmo--;
            //manager.text.text = reserveAmmo + "|" + currentAmmo;

            if (currentAmmo > 0)
            {
                canShoot = false;
                attackDelay = Time.time + 1f / manager.currentWeapon.fireRate;
            }
            else
            {
                if (reserveAmmo == 0)
                {
                    manager.ReturnToDefaultWeapon();
                }
                manager.OnReload();
            }
        }

    }

    void ShootShotGun(Player_FPS manager)
    {     
        RaycastHit hit;
        Zombie_Health zombieHealth = null;
        for (int i = 0; i < 7; i++)
        {
            Vector3 horiSpread = Vector3.right * Random.Range(-0.7f, 0.7f);
            Vector3 vertSpread = Vector3.up * Random.Range(-0.7f, 0.7f);

            if (Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward + (horiSpread + vertSpread), out hit, manager.currentWeapon.range))
            {
                if (hit.transform.CompareTag("Zombie/Turso") || hit.transform.CompareTag("Zombie/Head"))
                {
                    zombieHealth = hit.transform.GetComponentInParent<Zombie_Health>();
                    zombieHealth.TakeDamage(manager.currentWeapon.damage);
                    Debug.Log("Hit " + hit.transform.name);
                    manager.HelpInstantiate(manager.currentWeapon.impact, hit.point, Quaternion.Euler(hit.normal));
                }
                manager.SpawnBulletTrail(hit.point);
            }
            else
                manager.SpawnBulletTrail(manager.cam.transform.position + manager.cam.transform.forward * manager.currentWeapon.range + (horiSpread + vertSpread));
        }

    }

    public override IEnumerator Reload(Player_FPS manager)
    {
        Handheld.Vibrate();
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = manager.currentWeapon.magCapacity;
        reserveAmmo--;
        //manager.text.text = reserveAmmo + "|" + currentAmmo;
    }

}
