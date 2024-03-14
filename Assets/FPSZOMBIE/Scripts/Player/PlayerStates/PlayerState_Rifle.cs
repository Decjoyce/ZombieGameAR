using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerState_Rifle : PlayerState_Base
{
    float attackDelay;
    bool canShoot;
    float reloadSpeed;
    int currentAmmo;
    int reserveAmmo;

    GameObject ammoCounter;
    TextMeshProUGUI reserveUI;

    public override void EnterState(Player_FPS manager)
    {
        currentAmmo = manager.currentWeapon.magCapacity;
        reloadSpeed = manager.currentWeapon.reloadSpeed;

        ammoCounter = manager.HelpInstantiateAsChild(manager.currentWeapon.ammoCounter, manager.ammo);
        reserveUI = ammoCounter.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        reserveUI.text = reserveAmmo.ToString();
    }

    public override void ExitState(Player_FPS manager)
    {
        manager.HelpDestroy(ammoCounter);
        manager.StopReload();
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
        if(canShoot && currentAmmo > 0)
        {
            manager.audio.PlayOneShot(manager.clip);

            ShootGun(manager);

            currentAmmo--;
            ammoCounter.transform.GetChild(currentAmmo + 1).gameObject.SetActive(false);

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

    void ShootGun(Player_FPS manager)
    {
        RaycastHit hit;
        if (Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward, out hit, manager.currentWeapon.range))
        {
            if (hit.transform.CompareTag("Zombie/Turso"))
            {
                Zombie_Health zombieHealth = hit.transform.parent.GetComponent<Zombie_Health>();
                zombieHealth.TakeDamage(manager.currentWeapon.damage);

                manager.HelpInstantiate(manager.currentWeapon.impact, hit.point, Quaternion.Euler(hit.normal));
            }
            if (hit.transform.CompareTag("Zombie/Head"))
            {
                Zombie_Health zombieHealth = hit.transform.parent.GetComponent<Zombie_Health>();
                zombieHealth.TakeDamage(manager.currentWeapon.damage * 2, headShot: true);

                manager.HelpInstantiate(manager.currentWeapon.impact, hit.point, Quaternion.Euler(hit.normal));
            }

            Debug.Log("HIT");
            manager.SpawnBulletTrail(hit.point);
        }
        else
            manager.SpawnBulletTrail(manager.cam.transform.position + manager.cam.transform.forward * manager.currentWeapon.range);
    }

    public override IEnumerator Reload(Player_FPS manager)
    {
        Handheld.Vibrate();
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = manager.currentWeapon.magCapacity;
        reserveAmmo--;
        reserveUI.text = reserveAmmo.ToString();
        for (int i = 1; i < ammoCounter.transform.childCount; i++)
        {
            ammoCounter.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
