using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    [SerializeField] float spawnDelay;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(zombie, transform.position, transform.rotation);
        StartCoroutine(SpawnZombie());
    }

    IEnumerator SpawnZombie()
    {
        yield return new WaitForSecondsRealtime(spawnDelay);
        Instantiate(zombie, transform.position, transform.rotation);
        StartCoroutine(SpawnZombie());
    }

}
