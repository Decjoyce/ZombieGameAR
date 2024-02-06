using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_FPS : MonoBehaviour, PlayerControls.IBaseControlsActions
{
    Shoot shoot;
    PlayerControls playerControls;

    public Transform cam;
    [SerializeField] Transform firePoint;
    public GameObject effects;
    [SerializeField] GameObject bulletTrail;

    //States
    PlayerState_Base currentState;
    public PlayerState_Pistol state_Pistol = new PlayerState_Pistol();
    public PlayerState_Shotgun state_Shotgun = new PlayerState_Shotgun();

    //Weapons
    public WeaponType_Base currentWeapon;
    public WeaponType_Base defaultWeapon;
    public TextMeshProUGUI text;

    void Awake()
    {
        playerControls = new PlayerControls();

        playerControls.BaseControls.SetCallbacks(this);

        shoot = GetComponent<Shoot>();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        currentState = state_Shotgun;
        currentState.EnterState(this);
    }

    private void Update()
    {
        transform.position = cam.transform.position;
        currentState.FrameUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }

    public void SwitchState(string newState)
    {
        currentState.ExitState(this);
        switch (newState)
        {
            case "PISTOL":
                currentState = state_Pistol;
                break;
            case "Shotgun":
                currentState = state_Shotgun;
                break;
            default:
                Debug.LogError("INVALID STATE: " + newState);
                break;
        }
        currentState.EnterState(this);
    }

    public void OnSingleTouch(InputAction.CallbackContext context)
    {
        if (context.performed)
            currentState.TouchInput(this);
    }

    public void PickUpWeapon(WeaponType_Base newWeapon)
    {
        currentWeapon = newWeapon;
        SwitchState(currentWeapon.type);
    }

    public void SpawnBulletTrail(Vector3 endPos)
    {
        GameObject newBulletTrail = Instantiate(bulletTrail, cam.transform.position, cam.transform.rotation, effects.transform);
        LineRenderer trail = newBulletTrail.GetComponent<LineRenderer>();
        trail.SetPosition(0, firePoint.position);
        trail.SetPosition(1, cam.transform.position + endPos);
        trail.widthMultiplier = currentWeapon.trailSize;
        trail.colorGradient = currentWeapon.trailColor;
        Destroy(newBulletTrail, 2f);
    }

    public void DebugShoot()
    {
        currentState.TouchInput(this);
    }
}
