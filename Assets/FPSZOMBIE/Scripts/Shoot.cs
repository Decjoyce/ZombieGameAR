using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Zombie_Health zombieHealth;
    public GameObject projectile;
    public GameObject Explosion;
    public Transform Camera;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        zombieHealth = GetComponent<Zombie_Health>();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit))
            {
                zombieHealth.currentHealth -= 1;
                Destroy(hit.transform.gameObject);
                Instantiate(Explosion, hit.transform.position, hit.transform.rotation);
                Destroy(Explosion, 2f);
                Debug.Log("Shoot");
            
            }
        }
    }
}
