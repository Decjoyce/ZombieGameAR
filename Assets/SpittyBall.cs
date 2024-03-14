using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittyBall : MonoBehaviour
{
    [SerializeField] float damage;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        Debug.Log(other.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
