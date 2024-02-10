using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        gm = GetComponent<GameManager>();
    }

    [SerializeField] GameObject zombiePrefab;


    [SerializeField] Transform arPlane;
    MeshFilter[] arPlanes;
    private List<GameObject> zombies = new List<GameObject>();


    int numZombies = 6;
    [SerializeField] int ogNumZombies;
    [SerializeField] int zombieIncreaseMultiplier;

    GameManager gm;

    private void Start()
    {
        numZombies = ogNumZombies;
    }

    public void StartSpawningZombies()
    {
        for (int i = 0; i < numZombies; i++)
        {
            StartCoroutine(SpawnZombie(i));
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
    }
    
    IEnumerator SpawnZombie(int i)
    {
        float ranDelay = Random.Range(1f, i * 5f);
        yield return new WaitForSecondsRealtime(ranDelay);
        Vector3 spawnPos = RandomPointOnARPlane();
        GameObject newZombie = Instantiate(zombiePrefab, spawnPos, transform.rotation);
        zombies.Add(newZombie);
    }

    public void CalculateNumberZombies()
    {
        numZombies = ogNumZombies + gm.wave * zombieIncreaseMultiplier;
    }

    public Vector3 RandomPointOnARPlane()
    {
        arPlanes = arPlane.GetChild(1).GetComponentsInChildren<MeshFilter>();
        Mesh m = arPlanes[Random.Range(0, arPlanes.Length)].mesh;
        Vector3 spawnPos = m.vertices[Random.Range(0, m.vertices.Length)];
        return spawnPos;
    }

}
