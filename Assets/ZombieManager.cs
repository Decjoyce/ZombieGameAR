using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of ZombieWaves Found");
            return;
        }
        instance = this;
    }

    [SerializeField] GameObject zombiePrefab;

    [SerializeField] Transform origin;
    MeshFilter[] arPlanes;
    private Vector3[] zombieSpawners;
    private List<GameObject> zombies = new();


    [SerializeField] int numZombieSpawners = 6;
    [SerializeField] int maxZombiesPerRound = 2;
    [SerializeField] int maxZombiesAllowedAtOnce = 12;
    [SerializeField] float spawnFrequency;


    int totalZombiesSpawned;

    public bool isTimed;

    int zombiesKilledInRound;
    int zombiesSpawnedThisRound;

    private void Start()
    {
        zombieSpawners = new Vector3[numZombieSpawners];
    }

    public void StartSpawningZombies()
    {
        for(int i = 0; i < numZombieSpawners; i++)
        {
            zombieSpawners[i] = RandomPointOnARPlane();
            StartCoroutine(SpawnZombie(i * 4f, i));
        }
    }

    public void StopSpawningZombies()
    {
        StopAllCoroutines();
        foreach(GameObject zombie in zombies)
        {
            if(zombie != null)
                Destroy(zombie);
        }
        zombies.RemoveRange(0, zombies.Count);
        Debug.Log(totalZombiesSpawned);
    }
    
    IEnumerator SpawnZombie(float delay, int spawner)
    {
        yield return new WaitForSecondsRealtime(delay);
            GameObject zombie = Instantiate(zombiePrefab, zombieSpawners[spawner], Quaternion.identity);
            zombies.Add(zombie);
            zombiesSpawnedThisRound++;
            totalZombiesSpawned++;
    }

    public void ZombieDead(GameObject theZombie)
    {
        zombies.Remove(theZombie);
        if (zombiesSpawnedThisRound + 1 < maxZombiesPerRound)
        {
            float ranDelay = Random.Range(2f, 5f);
            int ranSpawner = Random.Range(0, zombieSpawners.Length);
            StartCoroutine(SpawnZombie(ranDelay, ranSpawner));
        }
    }

    public Vector3 RandomPointOnARPlane()
    {
        arPlanes = origin.GetChild(1).GetComponentsInChildren<MeshFilter>();
        MeshFilter arPlane = arPlanes[Random.Range(0, arPlanes.Length)];
        Mesh m = arPlane.mesh;
        float xPos = Random.Range(-m.bounds.size.x, m.bounds.extents.x);
        float zPos = Random.Range(-m.bounds.size.z, m.bounds.extents.z);
        Vector3 spawnPos = m.bounds.ClosestPoint(new Vector3(arPlane.transform.position.x + xPos, arPlane.transform.position.y, arPlane.transform.position.z + zPos));
        return spawnPos;
    }

    public void CalculateNumberZombies(int currentWave)
    {
        switch (currentWave)
        {
            case 1:
                
                break;
            case 3:
                //12
                break;
            case 10:
                //18
                break;
        }
    }

}
