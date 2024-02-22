using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponType_Base weapon;
    public ScoreManager scoreManager;
    public int cost;

    public bool CheckPrice()
    {
        return ScoreManager.instance.currentScore >= cost;
    }

    public void PickedUpWeapon()
    {
            ScoreManager.instance.DecreaseScore(cost);
            Destroy(gameObject);
    }
}
