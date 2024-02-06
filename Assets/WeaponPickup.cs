using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponType_Base weapon;
    public void PickedUpWeapon()
    {
        Destroy(gameObject);
    }
}
