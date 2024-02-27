using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    public PlayerState_Dead state_Dead = new PlayerState_Dead();
    public PlayerState_Hand state_Hand = new PlayerState_Hand();
    public PlayerState_Pistol state_Pistol = new PlayerState_Pistol();
    public PlayerState_Shotgun state_Shotgun = new PlayerState_Shotgun();
    public PlayerState_Bazooka state_Bazooka = new PlayerState_Bazooka();

    //Weapons
    public WeaponType_Base currentWeapon;
    public WeaponType_Base defaultWeapon;
    public Image crosshairs;
    public Sprite handCrosshair;
    public TextMeshProUGUI text;
    public TextMeshProUGUI costText;

    float attackDelay;
    bool canShoot;
    float reloadDelay;
    int currentAmmo;
    int reserveAmmo;
    bool emptyGun;

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
        currentState = state_Pistol;
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
            case "DEAD":
                currentState = state_Dead;
                break;
            case "HAND":
                currentState = state_Hand;
                crosshairs.sprite = handCrosshair;
                break;
            case "PISTOL":
                currentState = state_Pistol;
                break;
            case "SHOTGUN":
                currentState = state_Shotgun;
                break;
            case "BAZOOKA":
                currentState = state_Bazooka;
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

    public void OnReload()
    {
        StartCoroutine(currentState.Reload(this));
    }


    public void PickUpWeapon(WeaponType_Base newWeapon)
    {
        currentWeapon = newWeapon;
        //crosshairs.sprite = newWeapon.crosshair;
        //SwitchState(currentWeapon.type);
    }

    public void ReturnToCurrentWeaponState()
    {
        crosshairs.sprite = currentWeapon.crosshair;
        SwitchState(currentWeapon.type);
    }

    public void ReturnToDefaultWeapon()
    {
        currentWeapon = defaultWeapon;
        SwitchState(currentWeapon.type);
    }

    public void HelpInstantiate(GameObject obj, Vector3 pos, Quaternion rot)
    {
        Instantiate(obj, pos, rot);
    }

    public void SpawnBulletTrail(Vector3 endPos)
    {
        GameObject newBulletTrail = Instantiate(bulletTrail, cam.transform.position, cam.transform.rotation, effects.transform);
        LineRenderer trail = newBulletTrail.GetComponent<LineRenderer>();
        trail.SetPosition(0, firePoint.position);
        trail.SetPosition(1, endPos);
        trail.widthMultiplier = currentWeapon.trailSize;
        trail.colorGradient = currentWeapon.trailColor;
        Destroy(newBulletTrail, 2f);
    }

    public void DebugShoot()
    {
        currentState.TouchInput(this);
    }
}
