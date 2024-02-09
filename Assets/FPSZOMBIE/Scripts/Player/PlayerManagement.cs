using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    public static PlayerManagement instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of PlayerManagement Found");
            return;
        }
        instance = this;
    }

    public GameObject player;
    public PlayerHealth playerHealth;
}
