using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_FPS : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;

    ZombieState_Base currentState;
    public ZombieState_Follow state_follow = new ZombieState_Follow();
    public ZombieState_Attack state_attack = new ZombieState_Attack();

    public float speed_movement;
    public float speed_attack;
    public float damage;

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
                currentState.EnterState(this);
                break;
            case "ATTACK":
                currentState = state_attack;
                break;
            default:
                Debug.LogError("INVALID STATE: " + newState);
                break;
        }
        currentState.EnterState(this);
    }


}
