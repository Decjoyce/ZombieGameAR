using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Zombie_Spitter : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;
    public Animator anim;

    public Zombie_Health zombieHealth;

    SpitterState_Base currentState;
    public SpitterState_Follow state_follow = new SpitterState_Follow();
    public SpitterState_Attack state_attack = new SpitterState_Attack();
    public SpitterState_Stagger state_stagger = new SpitterState_Stagger();
    public SpitterState_Dead state_dead = new SpitterState_Dead();

    public GameObject[] availableDrops;
    [SerializeField] float dropChance;

    public float speed_movement;
    public float speed_attack;
    public float damage;
    public float staggerTime;
    public float throwForce;
    public float throwUpwardForce;
    public int totalThrows;
    public Transform ThrowRotation;
    public Transform attackPoint;
    public GameObject objectToThrow;


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
            case "DEAD":
                currentState = state_dead;
                break;
            default:
                Debug.LogError("INVALID STATE: " + newState);
                break;
        }
        currentState.EnterState(this);
    }

    public GameObject HelpInstantiate(GameObject newObj, Vector3 pos, Quaternion rot)
    {
        return Instantiate(newObj, pos, rot);
    }
}
