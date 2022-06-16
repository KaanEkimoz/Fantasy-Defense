using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField]private float spawnRate = 12f;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] pawnsToSpawn;
    void Start()
    {
        spawnRate = 5f;
        InvokeRepeating(nameof(SpawnRandomEnemies), spawnRate,spawnRate);
    }
    private void SpawnRandomEnemies()
    {
        int randomSpawnIndex = Random.Range(0, pawnsToSpawn.Length);
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(pawnsToSpawn[randomSpawnIndex],spawnPoints[randomSpawnPoint].transform.position,Quaternion.identity);
    }
}
