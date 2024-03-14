using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState_Bazooka : PlayerState_Base
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
        reserveAmmo = manager.currentWeapon.reserveAmmo;
        reloadSpeed = manager.currentWeapon.reloadSpeed;
        ammoCounter = manager.HelpInstantiateAsChild(manager.currentWeapon.ammoCounter, manager.ammo);
        reserveUI = manager.ammo.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
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
            ShootRocket(manager);

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
        Handheld.Vibrate();
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = manager.currentWeapon.magCapacity;
        reserveAmmo--;
        reserveUI.text = reserveAmmo.ToString();
        ammoCounter.transform.GetChild(1).gameObject.SetActive(true);
    }

}
