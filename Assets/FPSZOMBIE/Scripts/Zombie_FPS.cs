using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_FPS : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;

    public Zombie_Health zombieHealth;

    ZombieState_Base currentState;
    public ZombieState_Follow state_follow = new ZombieState_Follow();
    public ZombieState_Attack state_attack = new ZombieState_Attack();
    public ZombieState_Stagger state_stagger = new ZombieState_Stagger();

    public GameObject[] availableDrops;
    [SerializeField] float dropChance;

    public float speed_movement;
    public float speed_attack;
    public float damage;
    public float staggerTime;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManagement.instance.player;
        playerHealth = PlayerManagement.instance.playerHealth;

        currentState = state_follow;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.FrameUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
    }

    public void SwitchState(string newState)
    {
        currentState.ExitState(this);
        switch (newState)
        {
            case "FOLLOW":
                currentState = state_follow;
                break;
            case "ATTACK":
                currentState = state_attack;
                break;
            case "STAGGER":
                currentState = state_stagger;
                break;
            default:
                Debug.LogError("INVALID STATE: " + newState);
                break;
        }
        currentState.EnterState(this);
    }

    public void HandleDrops()
    {
        if(Random.value <= dropChance)
            Instantiate(availableDrops[Random.Range(0, availableDrops.Length)], transform.position, transform.rotation);
    }
}
