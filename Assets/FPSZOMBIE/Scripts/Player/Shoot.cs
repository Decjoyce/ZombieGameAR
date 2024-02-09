using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject Explosion;
    public Transform Camera;
    RaycastHit hit;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootGun()
    {
        DebugTextDisplayer.instance.ChangeText("Shot");
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("Zombie"))
            {
                Zombie_FPS hitZombie = hit.transform.GetComponent<Zombie_FPS>();
                hitZombie.zombieHealth.TakeDamage(1);
                //Destroy(hit.transform.gameObject);
                GameObject newExplosion = Instantiate(Explosion, hit.point, hit.transform.rotation);
                Destroy(newExplosion, 0.5f);
                DebugTextDisplayer.instance.ChangeText("Hit Zombie");
            }
        }
    }
}
