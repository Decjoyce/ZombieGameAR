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
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(readyToThrow);
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
        Destroy(gameObject, DestroyCooldown);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(readyToThrow == true && other.CompareTag("Player"))
        {
            Throw();
            readyToThrow = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && readyToThrow == false)
        {
            readyToThrow = true;
        }
        
    }
}
