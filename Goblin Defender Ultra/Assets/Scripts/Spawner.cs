using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;

    [SerializeField] private float maxEnemies; // maximum amount of enemies each level
    [SerializeField] private float enemiesPerSpawn; // amount of enemies each spawn session
    [SerializeField] private float spawnDelay; // delay for each spawn session

    private float spawnStartTime; // last spawn session
    private float currentEnemiesAmount;

    public float CurrentEnemiesAmount
    {
        get
        {
            return currentEnemiesAmount;
        }

        set
        {
            currentEnemiesAmount = value;
        }
    }

    // Use this for initialization
    void Start () {
        SpawnPerSessions();
        spawnStartTime = Time.time;
        currentEnemiesAmount = 0;

    }
	
	// Update is called once per frame
	void Update () {
        Spawn();
    }

    public void Spawn()
    {
        float timeSinceSpawn = Time.time - spawnStartTime;
        if (timeSinceSpawn >= spawnDelay)
        {
            SpawnPerSessions();
            spawnStartTime = Time.time;
        }
    }

    private void SpawnPerSessions()
    {
        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(transform.position.x + 2f * i, transform.position.y, transform.position.z);
            enemy.transform.rotation = Quaternion.identity;
            currentEnemiesAmount += 1;
        }
    }
}
