using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Weapon", menuName = "Weapons/New Base Weapon")]
public class WeaponType_Base : ScriptableObject
{
    //Stats
    public int damage;
    public float fireRate;
    public float range;
    public float reloadSpeed;
    public int magCapacity;
    public bool unlimited;

    //Assets
    public Sprite crosshair;
    public GameObject impact;
}
