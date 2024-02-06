using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_FPS : MonoBehaviour, PlayerControls.IBaseControlsActions
{
    Shoot shoot;
    PlayerControls playerControls;

    public Transform cam;

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

    private void Update()
    {
        transform.position = cam.transform.position;
    }

    public void OnSingleTouch(InputAction.CallbackContext context)
    {
        if(context.performed)
            shoot.ShootGun();
    }
}
