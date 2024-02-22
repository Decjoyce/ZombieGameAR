using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    Player_FPS dad;
    [SerializeField] float maxHealth;
    [SerializeField] float regenDelay;
    [SerializeField] float regenRate;
    public float currentHealth;
    bool isRegen;

    [SerializeField] Image blood;

    private void Awake()
    {
        dad = GetComponent<Player_FPS>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isRegen)
        {
            currentHealth += regenRate * Time.deltaTime;
        }

        if(currentHealth >= maxHealth && isRegen == true)
        {
            isRegen = false;
            currentHealth = maxHealth;
            blood.color = new Color(255, 0, 0, 0);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        float bloodStrength = blood.color.a + amount / 100;
        blood.color = new Color(255, 0, 0, bloodStrength);
        isRegen = false;
        StopAllCoroutines();
        StartCoroutine(RegenDelay());
        if (currentHealth <= 0)
        {
            GameManager.instance.ChangeState("END");
            dad.SwitchState("DEAD");
        }
    }


    IEnumerator RegenDelay()
    {
        yield return new WaitForSecondsRealtime(regenDelay);
        isRegen = true;
    }
}
