using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePlaneSpawner : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    MeshFilter mf;
    Mesh m;
    [SerializeField] float spawnDelay;
    [SerializeField] float gridRes;
    [SerializeField] int maxZombies;
    int spawnedZomibes;

    private void Start()
    {
        mf = GetComponent<MeshFilter>();
        StartCoroutine(SpawnZombie_Grid());
    }

    IEnumerator SpawnZombie_Grid()
    {
        yield return new WaitForSecondsRealtime(spawnDelay);
        m = mf.mesh;
        Vector3 spawnPos = m.vertices[Random.Range(0, m.vertices.Length)];
        Instantiate(zombie, spawnPos, transform.rotation, transform);
        spawnedZomibes++;
        if(spawnedZomibes < maxZombies)
            StartCoroutine(SpawnZombie_Grid());
    }


}
