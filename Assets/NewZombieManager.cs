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
    Transform[] arPlanes;

    int[] numOfEnemy = new int[5];

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
                if(enemiesAlive[i] != null)
                    enemiesAlive[i].GetComponent<Zombie_Health>().TakeDamage(100000, false);
                
            }
            enemiesAlive.Clear();
        }

    }

    public void ZombieDead(GameObject theZombie)
    {
        //if (enemiesAlive.Contains(theZombie))
            //enemiesAlive.Remove(theZombie);
    }

    public void CalculateWave()
    {
        numOfEnemy = new int[5];
        if (gm.wave > 1)
            waveValue = gm.wave * waveValueMultiplier;
        else
            waveValue = 10;
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
                    if(enemies[randEnemyId].maxCount == 0 || numOfEnemy[randEnemyId] < enemies[randEnemyId].maxCount * (gm.wave - enemies[randEnemyId].requiredWaveToSpawn))
                    {
                    generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                    waveValue -= randEnemyCost;
                    numOfEnemy[randEnemyId]++;
                    }
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
        Transform arPlane = origin.GetChild(1).GetChild(Random.Range(0, origin.transform.childCount));
        ARPlane plane = arPlane.GetComponent<ARPlane>();
        Mesh m = arPlane.GetComponent<MeshFilter>().mesh;
        float xPos = Random.Range(-m.bounds.extents.x, m.bounds.extents.x);
        float zPos = Random.Range(-m.bounds.extents.z, m.bounds.extents.z);
        Debug.Log(plane.center + " ??d " + arPlane.position);
        Vector3 spawnPos = new Vector3(arPlane.position.x + xPos, arPlane.position.y, arPlane.position.z + zPos);
        Debug.Log(spawnPos);
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
