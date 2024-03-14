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
    [SerializeField] GameObject spitter_ball; 
    [SerializeField] GameObject doggo; 
    [SerializeField] GameObject bigZombie;
    [SerializeField] GameObject kingZombie;

    [SerializeField] TextMeshProUGUI[] normalZombie_Text;
    [SerializeField] TextMeshProUGUI[] runner_Text;
    [SerializeField] TextMeshProUGUI[] spitter_Text;
    [SerializeField] TextMeshProUGUI[] doggo_Text;
    [SerializeField] TextMeshProUGUI[] bigZombie_Text;
    [SerializeField] TextMeshProUGUI[] kingZombie_Text;

    int normalZombie_Difficulty = 3, runner_Difficulty = 2, spitter_Difficulty = 2, doggo_Difficulty = 1, bigZombie_Difficulty = 12, kingZombie_Difficulty = 50;
    int normalZombie_Speed = 2, runner_Speed = 10, spitter_Speed = 8, doggo_Speed = 20, bigZombie_Speed = 1, kingZombie_Speed = 4;
    int normalZombie_Damage = 5, runner_Damage = 5, spitter_Damage = 10, doggo_Damage = 10, bigZombie_Damage = 30, kingZombie_Damage = 50;

    float GetSpeed(int i)
    {
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
            if (normalZombie_Difficulty > 50)
                normalZombie_Difficulty = 1;
        }
        else
        {
            normalZombie_Difficulty--;
            if (normalZombie_Difficulty < 1)
                normalZombie_Difficulty = 50;
        }

        normalZombie.GetComponent<Zombie_Health>().health = normalZombie_Difficulty;
        normalZombie_Text[0].text = "Health: " + normalZombie_Difficulty;
    }

    public void ChangeNormalZombieSpeed(bool minus)
    {
        if (!minus)
        {
            normalZombie_Speed++;
            if (normalZombie_Speed > 20)
                normalZombie_Speed = 0;
        }
        else
        {
            normalZombie_Speed--;
            if (normalZombie_Speed < 0)
                normalZombie_Speed = 20;
        }

        float newSpeed = GetSpeed(normalZombie_Speed);
        normalZombie.GetComponent<Zombie_FPS>().speed_movement = newSpeed;
        normalZombie_Text[1].text = "Speed: " + newSpeed.ToString("0.00");
    }

    public void ChangeNormalZombieDamage(bool minus)
    {
        if (!minus)
        {
            normalZombie_Damage++;
            if (normalZombie_Damage > 100)
                normalZombie_Damage = 0;
        }
        else
        {
            normalZombie_Damage--;
            if (normalZombie_Damage < 0)
                normalZombie_Damage = 100;
        }

        int newDam = normalZombie_Damage;
        normalZombie.GetComponent<Zombie_FPS>().damage = newDam;
        normalZombie_Text[2].text = "Damage: " + newDam;
    }

    public void ChangeRunnerDifficulty(bool minus)
    {
        if (!minus)
        {
            runner_Difficulty++;
            if (runner_Difficulty > 50)
                runner_Difficulty = 1;
        }
        else
        {
            runner_Difficulty--;
            if (runner_Difficulty < 1)
                runner_Difficulty = 50;
        }

        runner.GetComponent<Zombie_Health>().health = runner_Difficulty;
        runner_Text[0].text = "Health: " + runner_Difficulty;
    }

    public void ChangeRunnerSpeed(bool minus)
    {
        if (!minus)
        {
            runner_Speed++;
            if (runner_Speed > 20)
                runner_Speed = 0;
        }
        else
        {
            runner_Speed--;
            if (runner_Speed < 0)
                runner_Speed = 20;
        }

        float newSpeed = GetSpeed(runner_Speed);
        runner.GetComponent<Zombie_FPS>().speed_movement = newSpeed;
        runner_Text[1].text = "Speed: " + newSpeed.ToString("0.00");
    }

    public void ChangeRunnerDamage(bool minus)
    {
        if (!minus)
        {
            runner_Damage++;
            if (runner_Damage > 100)
                runner_Damage = 0;
        }
        else
        {
            runner_Damage--;
            if (runner_Damage < 0)
                runner_Damage = 100;
        }

        int newDam = runner_Damage;
        runner.GetComponent<Zombie_FPS>().damage = newDam;
        runner_Text[2].text = "Damage: " + newDam;
    }

    public void ChangeSpitterDifficulty(bool minus)
    {
        if (!minus)
        {
            spitter_Difficulty++;
            if (spitter_Difficulty > 50)
                spitter_Difficulty = 1;
        }
        else
        {
            spitter_Difficulty--;
            if (spitter_Difficulty < 1)
                spitter_Difficulty = 50;
        }

        spitter.GetComponent<Zombie_Health>().health = spitter_Difficulty;
        spitter_Text[0].text = "Health: " + spitter_Difficulty;
    }

    public void ChangeSpitterSpeed(bool minus)
    {
        if (!minus)
        {
            spitter_Speed++;
            if (spitter_Speed > 20)
                spitter_Speed = 0;
        }
        else
        {
            spitter_Speed--;
            if (spitter_Speed < 0)
                spitter_Speed = 20;
        }

        float newSpeed = GetSpeed(spitter_Speed);
        spitter.GetComponent<Zombie_Spitter>().speed_movement = newSpeed;
        spitter_Text[1].text = "Speed: " + newSpeed.ToString("0.00");
    }

    public void ChangeSpitterDamage(bool minus)
    {
        if (!minus)
        {
            spitter_Damage++;
            if (spitter_Damage > 100)
                spitter_Damage = 0;
        }
        else
        {
            spitter_Damage--;
            if (spitter_Damage < 0)
                spitter_Damage = 100;
        }

        int newDam = spitter_Damage;
        spitter_ball.GetComponent<SpittyBall>().damage = newDam;
        spitter_Text[2].text = "Damage: " + newDam;
    }

    public void ChangeDoggoDifficulty(bool minus)
    {
        if (!minus)
        {
            doggo_Difficulty++;
            if (doggo_Difficulty > 50)
                doggo_Difficulty = 1;
        }
        else
        {
            doggo_Difficulty--;
            if (doggo_Difficulty < 1)
                doggo_Difficulty = 50;
        }

        doggo.GetComponent<Zombie_Health>().health = doggo_Difficulty;
        doggo_Text[0].text = "Health: " + doggo_Difficulty;
    }

    public void ChangeDoggoSpeed(bool minus)
    {
        if (!minus)
        {
            doggo_Speed++;
            if (doggo_Speed > 20)
                doggo_Speed = 0;
        }
        else
        {
            doggo_Speed--;
            if (doggo_Speed < 0)
                doggo_Speed = 20;
        }

        float newSpeed = GetSpeed(doggo_Speed);
        doggo.GetComponent<Zombie_FPS>().speed_movement = newSpeed;
        doggo_Text[1].text = "Speed: " + newSpeed.ToString("0.00");
    }

    public void ChangeDoggoDamage(bool minus)
    {
        if (!minus)
        {
            doggo_Damage++;
            if (doggo_Damage > 100)
                doggo_Damage = 0;
        }
        else
        {
            doggo_Damage--;
            if (doggo_Damage < 0)
                doggo_Damage = 100;
        }

        int newDam = doggo_Damage;
        doggo.GetComponent<Zombie_FPS>().damage = newDam;
        doggo_Text[2].text = "Damage: " + newDam;
    }

    public void ChangeBigZombieDifficulty(bool minus)
    {
        if (!minus)
        {
            bigZombie_Difficulty++;
            if (bigZombie_Difficulty > 50)
                bigZombie_Difficulty = 1;
        }
        else
        {
            bigZombie_Difficulty--;
            if (bigZombie_Difficulty < 1)
                bigZombie_Difficulty = 50;
        }

        bigZombie.GetComponent<Zombie_Health>().health = bigZombie_Difficulty;
        bigZombie_Text[0].text = "Health: " + bigZombie_Difficulty;
    }

    public void ChangeBigZombieSpeed(bool minus)
    {
        if (!minus)
        {
            bigZombie_Speed++;
            if (bigZombie_Speed > 20)
                bigZombie_Speed = 0;
        }
        else
        {
            bigZombie_Speed--;
            if (bigZombie_Speed < 0)
                bigZombie_Speed = 20;
        }

        float newSpeed = GetSpeed(bigZombie_Speed);
        bigZombie.GetComponent<Zombie_FPS>().speed_movement = newSpeed;
        bigZombie_Text[1].text = "Speed: " + newSpeed.ToString("0.00");
    }

    public void ChangeBigZombieDamage(bool minus)
    {
        if (!minus)
        {
            bigZombie_Damage++;
            if (bigZombie_Damage > 100)
                bigZombie_Damage = 0;
        }
        else
        {
            bigZombie_Damage--;
            if (bigZombie_Damage < 0)
                bigZombie_Damage = 100;
        }

        int newDam = bigZombie_Damage;
        bigZombie.GetComponent<Zombie_FPS>().damage = newDam;
        bigZombie_Text[2].text = "Damage: " + newDam;
    }

    public void ChangeKingZombieDifficulty(bool minus)
    {
        if (!minus)
        {
            kingZombie_Difficulty++;
            if (kingZombie_Difficulty > 50)
                kingZombie_Difficulty = 1;
        }
        else
        {
            kingZombie_Difficulty--;
            if (kingZombie_Difficulty < 1)
                kingZombie_Difficulty = 50;
        }

        kingZombie.GetComponent<Zombie_Health>().health = kingZombie_Difficulty;
        kingZombie_Text[0].text = "Health: " + kingZombie_Difficulty;
    }

    public void ChangeKingZombieSpeed(bool minus)
    {
        if (!minus)
        {
            kingZombie_Speed++;
            if (kingZombie_Speed > 20)
                kingZombie_Speed = 0;
        }
        else
        {
            kingZombie_Speed--;
            if (kingZombie_Speed < 0)
                kingZombie_Speed = 20;
        }

        float newSpeed = GetSpeed(kingZombie_Speed);
        kingZombie.GetComponent<Zombie_FPS>().speed_movement = newSpeed;
        kingZombie_Text[1].text = "Speed: " + newSpeed.ToString("0.00");
    }

    public void ChangeKingZombieDamage(bool minus)
    {
        if (!minus)
        {
            kingZombie_Damage++;
            if (kingZombie_Damage > 100)
                kingZombie_Damage = 0;
        }
        else
        {
            kingZombie_Damage--;
            if (kingZombie_Damage < 0)
                kingZombie_Damage = 100;
        }

        int newDam = kingZombie_Damage;
        kingZombie.GetComponent<Zombie_FPS>().damage = newDam;
        kingZombie_Text[2].text = "Damage: " + newDam;
    }
    #endregion
}
