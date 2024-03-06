using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Pickup : MonoBehaviour
{
    [SerializeField] private float healthIncrease;
    private PlayerHealth playerHealth;
    public GameObject Player;
    // Start is called before the first frame update
    private void Awake()
    {
        playerHealth = Player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && playerHealth.currentHealth > 0f)
        {

            if(playerHealth.currentHealth < playerHealth.maxHealth) 
            {
                playerHealth.currentHealth += healthIncrease;
                Destroy(gameObject);
                Debug.Log(playerHealth.currentHealth);
            }
        }
    }
}
