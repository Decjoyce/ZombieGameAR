using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Pickup : MonoBehaviour
{
    [SerializeField] private float healthIncrease;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    private void Start()
    {
        playerHealth = GameManager.instance.player.health;
        Destroy(gameObject, 15f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playerHealth.currentHealth > 0f && playerHealth.currentHealth < playerHealth.maxHealth)
        {
            playerHealth.currentHealth += healthIncrease;
            if (playerHealth.currentHealth > playerHealth.maxHealth)
                playerHealth.currentHealth = playerHealth.maxHealth;
            Destroy(gameObject);
            Debug.Log(playerHealth.currentHealth);
        }
    }
}
