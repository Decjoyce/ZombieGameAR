using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] uiText, uiAmmo;
    [SerializeField] Image crosshair, damageIndicator;

    [SerializeField] TextMeshProUGUI[] uiText_Showcase, uiAmmo_Showcase;
    [SerializeField] Image crosshair_Showcase, damageIndicator_Showcase;

    int uiText_Colour, uiAmmo_Colour, crosshair_Colour, damageIndicator_Colour;
    int crosshair_Size = 3;

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
        foreach (TextMeshProUGUI text in uiAmmo)
        {
            text.color = GetColor(uiAmmo_Colour);
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
                crosshair_Colour = 6;
        }
        crosshair.color = GetColor(crosshair_Colour);
        crosshair_Showcase.color = GetColor(crosshair_Colour);
    }

    public void ChangeCrosshairSize(bool minus)
    {
        if (!minus)
        {
            crosshair_Size++;
            if (crosshair_Size > 7)
                crosshair_Size = 7;
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
            if (damageIndicator_Colour > 9)
                damageIndicator_Colour = 0;
        }
        else
        {
            damageIndicator_Colour--;
            if (damageIndicator_Colour < 0)
                damageIndicator_Colour = 9;
        }
        damageIndicator.color = GetColor(damageIndicator_Colour);
    }
}
