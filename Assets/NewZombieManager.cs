using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class NewZombieManager : MonoBehaviour
{    
    GameManager gm;

    public List<Enemy> enemies = new List<Enemy>();
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> enemiesAlive = new List<GameObject>();
    int waveValue;
    [SerializeField] int waveValueMultiplier;

    bool allowSpawning;
    public float spawnTimer;
    public float spawnInterval;

    [SerializeField] Transform origin;
    MeshFilter[] arPlanes;

    public static NewZombieManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of ZombieWaves Found");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        gm = GameManager.instance;
        //CalculateWave();
    }

    private void Update()
    {
        if (allowSpawning)
        {

            if (spawnTimer <= 0)
            {
                if (enemiesToSpawn.Count > 0)
                {
                    GameObject zombie = Instantiate(enemiesToSpawn[0], RandomPointOnARPlane(), Quaternion.identity);
                    enemiesAlive.Add(zombie);
                    enemiesToSpawn.RemoveAt(0);
                    spawnTimer = spawnInterval;
                }
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }

    public void StartSpawningZombies()
    {
        allowSpawning = true;
        CalculateWave();
    }

    public void StopSpawningZombies(bool clearZombies = false)
    {
        allowSpawning = false;

        if (clearZombies)
        {
            ClearZombies();
        }
    }

    public void ClearZombies()
    {
        if(enemiesAlive.Count > 0)
        {
            for(int i = 0; i < enemiesAlive.Count; i++)
            {            
                enemiesAlive[0].GetComponent<Zombie_Health>().TakeDamage(100000);
                enemiesAlive.RemoveAt(0);
            }
        }

    }

    public void ZombieDead(GameObject theZombie)
    {
        if (enemiesAlive.Contains(theZombie))
            enemiesAlive.Remove(theZombie);
    }

    public void CalculateWave()
    {
        waveValue = gm.wave * waveValueMultiplier;
        GenerateZombies();
        spawnInterval = gm.waveTime / enemiesToSpawn.Count;
        spawnTimer = spawnInterval;
    }

    public void GenerateZombies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                if (gm.wave >= enemies[randEnemyId].requiredWaveToSpawn)
                {
                    generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                    waveValue -= randEnemyCost;
                }

            }
            else if (waveValue <= 0)
                break;
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
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
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
    public int requiredWaveToSpawn;
    public int maxCount;
}
