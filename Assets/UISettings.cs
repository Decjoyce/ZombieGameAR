using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    #region UI
    [Header("UI")]
    public TextMeshProUGUI[] uiText;
    public GameObject[] uiAmmo; 
    public Image crosshair; 
    public GameObject damageIndicator;

    public TextMeshProUGUI uiText_Showcase;
    public Image[] uiAmmo_Showcase;
    public Image crosshair_Showcase, damageIndicator_Showcase;

    int uiText_Colour, uiAmmo_Colour, crosshair_Colour, damageIndicator_Colour;
    int crosshair_Size = 3, damageIndicator_Size = 1;

    Color GetColor(int col)
    {
        switch (col)
        {
            case 0:
                return Color.white;
            case 1:
                return Color.red;
            case 2:
                return Color.blue;
            case 3:
                return Color.green;
            case 4:
                return Color.yellow;
            case 5:
                return Color.magenta;
            case 6:
                return Color.cyan;
            case 7:
                return new Color(1, 0.5f, 0f);
            case 8:
                return Color.grey;
            case 9:
                return Color.black;
            case 10:
                return new Color(0.6f, 0f, 0f);
            default:
                return Color.white;
        }
    }

    Vector2 GetSize(int size)
    {
        switch (size)
        {
            case 0:
                return Vector2.one * 100f;
            case 1:
                return Vector2.one * 125f;
            case 2:
                return Vector2.one * 150f;
            case 3:
                return Vector2.one * 175f;
            case 4:
                return Vector2.one * 200f;
            case 5:
                return Vector2.one * 225f;
            case 6:
                return Vector2.one * 250f;
            case 7:
                return Vector2.one * 275f;
            case 8:
                return Vector2.one * 300f;
            default:
                return Vector2.one * 175f;
        }
    }

    public void ChangeTextColour(bool minus)
    {
        if (!minus)
        {
            uiText_Colour++;
            if (uiText_Colour > 9)
                uiText_Colour = 0;
        }
        else
        {
            uiText_Colour--;
            if (uiText_Colour < 0)
                uiText_Colour = 9;
        }
        
        foreach(TextMeshProUGUI text in uiText)
        {
            text.color = GetColor(uiText_Colour);
        }
        uiText_Showcase.color = GetColor(uiText_Colour);
    }

    public void ChangeAmmoColour(bool minus)
    {
        if (!minus)
        {
            uiAmmo_Colour++;
            if (uiAmmo_Colour > 9)
                uiAmmo_Colour = 0;
        }
        else
        {
            uiAmmo_Colour--;
            if (uiAmmo_Colour < 0)
                uiAmmo_Colour = 9;
        }

        foreach (GameObject ammo in uiAmmo)
        {
            ammo.GetComponent<AmmoHelper>().ChangeAmmoColor(GetColor(uiAmmo_Colour));
        }

        foreach (Image bullet in uiAmmo_Showcase)
        {
            bullet.color = GetColor(uiAmmo_Colour);
        }
    }

    public void ChangeCrosshairColour(bool minus)
    {
        if (!minus)
        {
            crosshair_Colour++;
            if (crosshair_Colour > 9)
                crosshair_Colour = 0;
        }
        else
        {
            crosshair_Colour--;
            if (crosshair_Colour < 0)
                crosshair_Colour = 9;
        }
        crosshair.color = GetColor(crosshair_Colour);
        crosshair_Showcase.color = GetColor(crosshair_Colour);
    }

    public void ChangeCrosshairSize(bool minus)
    {
        if (!minus)
        {
            crosshair_Size++;
            if (crosshair_Size > 8)
                crosshair_Size = 8;
        }
        else
        {
            crosshair_Size--;
            if (crosshair_Size < 0)
                crosshair_Size = 0;
        }
        crosshair.GetComponent<RectTransform>().sizeDelta = GetSize(crosshair_Size);
        crosshair_Showcase.GetComponent<RectTransform>().sizeDelta = GetSize(crosshair_Size);
    }

    public void ChangeDamageIndicatorColour(bool minus)
    {
        if (!minus)
        {
            damageIndicator_Colour++;
            if (damageIndicator_Colour > 10)
                damageIndicator_Colour = 0;
        }
        else
        {
            damageIndicator_Colour--;
            if (damageIndicator_Colour < 0)
                damageIndicator_Colour = 10;
        }
        damageIndicator.transform.GetChild(0).GetComponentInChildren<Image>().color = GetColor(damageIndicator_Colour);
        damageIndicator_Showcase.color = GetColor(damageIndicator_Colour);
    }

    public void ChangeDamageIndicatorSize(bool minus)
    {
        if (!minus)
        {
            damageIndicator_Size++;
            if (damageIndicator_Size > 3)
                damageIndicator_Size = 3;
        }
        else
        {
            damageIndicator_Size--;
            if (damageIndicator_Size < 1)
                damageIndicator_Size = 1;
        }
        damageIndicator.transform.GetChild(0).localScale = (Vector2.one * 0.5f) * damageIndicator_Size;
        damageIndicator_Showcase.GetComponent<RectTransform>().localScale = (Vector2.one * 0.5f) * damageIndicator_Size;
    }
    #endregion

    #region Gameplay

    [Header("Gameplay")]
    [SerializeField] GameObject normalZombie;
    [SerializeField] GameObject runner; 
    [SerializeField] GameObject spitter; 
    [SerializeField] GameObject doggo; 
    [SerializeField] GameObject bigZombie;
    [SerializeField] GameObject kingZombie;



    int normalZombie_Difficulty, runner_Difficulty, spitter_Difficulty, doggo_Difficulty, bigZombie_Difficulty, kingZombie_Difficulty;
    int normalZombie_Speed, runner_Speed, spitter_Speed, doggo_Speed, bigZombie_Speed, kingZombie_Speed;

    float GetSpeed(int i)
    {
        /*        switch (i)
                {
                    case 0:
                        return 0.01f;
                    case 1:
                        return 0.05f;
                    case 2:
                        return 0.1f;
                    case 3:
                        return 0.15f;
                    case 4:
                        return 0.2f;
                    case 5:
                        return 0.25f;
                    case 6:
                        return 0.3f;
                    case 7:
                        return 0.4f;
                    case 8:
                        return 0.5f;
                    default:
                        return 0.2f;
                }*/
        if (i == 0)
            return 0.01f;
        else
            return ExtensionMethods.Map(i, 1, 20, 0.05f, 1);
    }

    public void ChangeNormalZombieDifficulty(bool minus)
    {
        if (!minus)
        {
            normalZombie_Difficulty++;
            if (normalZombie_Difficulty > 3)
                normalZombie_Difficulty = 1;
        }
        else
        {
            normalZombie_Difficulty--;
            if (normalZombie_Difficulty < 1)
                normalZombie_Difficulty = 3;
        }

        normalZombie.GetComponent<Zombie_Health>().health = normalZombie_Difficulty;
    }

    public void ChangeNormalZombieSpeed(bool minus)
    {
        if (!minus)
        {
            normalZombie_Speed++;
            if (normalZombie_Speed > 3)
                normalZombie_Speed = 1;
        }
        else
        {
            normalZombie_Speed--;
            if (normalZombie_Speed < 1)
                normalZombie_Speed = 3;
        }

        normalZombie.GetComponent<Zombie_FPS>().speed_movement = GetSpeed(normalZombie_Speed);
        Debug.Log(GetSpeed(normalZombie_Speed));
    }
    #endregion
}
