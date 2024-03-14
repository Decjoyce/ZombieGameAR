using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int damage;

    private void Start()
    {
        Destroy(gameObject, 0.4f);
    }

    private void FixedUpdate()
    {
        transform.localScale += Vector3.one * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Zombie/Turso") || other.CompareTag("Zombie/Head"))
        {
            Zombie_Health zombieHealth = other.transform.parent.GetComponent<Zombie_Health>();
            zombieHealth.TakeDamage(damage);
        }
    }
}
