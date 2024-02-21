using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponType_Base weapon;
    public ScoreManager scoreManager;
    public bool canBuyShotgun;

    public void Update()
    {
        if (scoreManager.currentScore > 14)
        {
            canBuyShotgun =  true;
        }

        else
        {
            canBuyShotgun = false;
        }
    }
    public void PickedUpWeapon()
    {
        Destroy(gameObject);
        scoreManager.ShotgunBought();
    }
}
