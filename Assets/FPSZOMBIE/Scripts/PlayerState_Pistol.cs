using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Pistol : PlayerState_Base
{
    float attackDelay;
    bool canShoot;
    RaycastHit hit;
    public override void EnterState(Player_FPS manager)
    {

    }

    public override void ExitState(Player_FPS manager)
    {

    }

    public override void FrameUpdate(Player_FPS manager)
    {
        if (!canShoot && Time.time >= attackDelay)
        {
            canShoot = true;
        }
    }

    public override void OnTriggerEnter(Player_FPS manager, Collider other)
    {

    }

    public override void OnTriggerExit(Player_FPS manager, Collider other)
    {

    }

    public override void PhysicsUpdate(Player_FPS manager)
    {
    }

    public override void TouchInput(Player_FPS manager)
    {
        DebugTextDisplayer.instance.ChangeText("Shot");

        if (canShoot && Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("Zombie"))
            {
                Zombie_FPS hitZombie = hit.transform.GetComponent<Zombie_FPS>();
                hitZombie.zombieHealth.TakeDamage(manager.currentWeapon.damage);

                canShoot = false;
                attackDelay = Time.time + 1f / manager.currentWeapon.fireRate;

                //GameObject newExplosion = manager.Instantiate(manager.currentWeapon.impact, hit.point, hit.transform.rotation);
                //manager.Destroy(newExplosion, 0.5f);

                DebugTextDisplayer.instance.ChangeText("Hit Zombie");
            }
        }
    }
}
