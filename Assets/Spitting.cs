using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitting : MonoBehaviour
{
    [Header("References")]
    public Transform ThrowRotation;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public Transform PlayerTransform;
    [SerializeField] float EffectRadius;
    private Rigidbody rb;

    
    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;
    public float DestroyCooldown = 10f;


    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    private bool readyToThrow;


    private void Awake()
    {
        readyToThrow = true;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        PlayerTransform = PlayerManagement.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newRot = Quaternion.LookRotation(transform.position - PlayerTransform.transform.position, Vector3.up).eulerAngles;
        transform.eulerAngles = new Vector3(0, newRot.y, 0);
        var difference = transform.position - PlayerTransform.position;
        if(difference.magnitude < EffectRadius && readyToThrow == true)
        {
            Throw();
            readyToThrow = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }

        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void Throw()
    {
        readyToThrow = false;
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, ThrowRotation.rotation);

        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

        Vector3 ForceDir = ThrowRotation.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(ThrowRotation.position, ThrowRotation.forward, out hit, 500f))
        {
            ForceDir = (hit.point - attackPoint.position).normalized;
        }

        Vector3 forceToAdd = ForceDir * throwForce + transform.up * throwUpwardForce;

        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }


}
