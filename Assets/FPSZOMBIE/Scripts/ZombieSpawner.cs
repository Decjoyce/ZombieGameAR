using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    [SerializeField] float spawnDelay;
    bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator SpawnZombie()
    {
        yield return new WaitForSecondsRealtime(spawnDelay);
        Instantiate(zombie, transform.position, transform.rotation);
        StartCoroutine(SpawnZombie());
    }



}
